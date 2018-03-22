#! /usr/bin/env python

import tensorflow as tf
import numpy as np
from numpy import * 
import os
import time
import datetime
import data_helpers
from myNet import myLSTM
from tensorflow.contrib import learn
from sklearn.preprocessing import normalize

# Parameters
# ==================================================

# Data loading params
dev_sample_percentage = 0.1 # "Percentage of the training data to use for validation")
positive_data_file =  "./data/rt-polaritydata/rt-polarity.pos"# "Data source for the positive data."
negative_data_file = "./data/rt-polaritydata/rt-polarity.neg" # "Data source for the negative data.")


# Model Hyperparameters
embedding_dim = 128, #"Dimensionality of character embedding (default: 128)")
filter_sizes = "3,4,5"# "Comma-separated filter sizes (default: '3,4,5')")
num_filters = 128# "Number of filters per filter size (default: 128)")
dropout_keep_prob = 0.5, #"Dropout keep probability (default: 0.5)")
l2_reg_lambda =  0.0 #"L2 regularization lambda (default: 0.0)")


# Training parameters
batch_size =  16 #"Batch Size (default: 64)")
num_epochs =  200 # "Number of training epochs (default: 200)")
evaluate_every =  100 # "Evaluate model on dev set after this many steps (default: 100)")
checkpoint_every = 100 # "Save model after this many steps (default: 100)")
num_checkpoints =  5 #"Number of checkpoints to store (default: 5)")
# Misc Parameters
allow_soft_placement = True #"Allow device soft device placement")
log_device_placement =  False # "Log placement of ops on devices")




# Data Preparation
# ==================================================

# Load data
print("Loading data...")
x_text, y = data_helpers.load_data_and_labels(positive_data_file, negative_data_file)

# Build vocabulary
max_document_length = max([len(x.split(" ")) for x in x_text])
vocab_processor = learn.preprocessing.VocabularyProcessor(max_document_length)
x = np.array(list(vocab_processor.fit_transform(x_text)))

# Randomly shuffle data
np.random.seed(10)
shuffle_indices = np.random.permutation(np.arange(len(y)))
x_shuffled = x[shuffle_indices]
y_shuffled = y[shuffle_indices]

# Split train/test set
# TODO: This is very crude, should use cross-validation
dev_sample_index = -1 * int(dev_sample_percentage * float(len(y)))
x_train, x_dev = x_shuffled[:dev_sample_index], x_shuffled[dev_sample_index:]
y_train, y_dev = y_shuffled[:dev_sample_index], y_shuffled[dev_sample_index:]

for s in [x_train, x_dev, y_train, y_dev]:
    s = normalize(s)

del x, y, x_shuffled, y_shuffled

print("Vocabulary Size: {:d}".format(len(vocab_processor.vocabulary_)))
print("Train/Dev split: {:d}/{:d}".format(len(y_train), len(y_dev)))


# Training
# ==================================================

with tf.Graph().as_default():
    session_conf = tf.ConfigProto(
      allow_soft_placement=allow_soft_placement,
      log_device_placement=log_device_placement)
    sess = tf.Session(config=session_conf)
    with sess.as_default():
        lstm = myLSTM(
            sequence_length=x_train.shape[1],
            n_classes=y_train.shape[1],
            n_input = 1,
            n_hidden_units = 12,
            batch_size = 16
            )

        # Define Training procedure
        global_step = tf.Variable(0, name="global_step", trainable=False)
        optimizer = tf.train.AdamOptimizer(1e-3)
        grads_and_vars = optimizer.compute_gradients(lstm.loss)
        train_op = optimizer.apply_gradients(grads_and_vars, global_step=global_step)


        
        #Keep track of gradient values and sparsity (optional)
        grad_summaries = []
        for g, v in grads_and_vars:
            if g is not None:
                grad_hist_summary = tf.summary.histogram("{}/grad/hist".format(v.name), g)
                sparsity_summary = tf.summary.scalar("{}/grad/sparsity".format(v.name), tf.nn.zero_fraction(g))
                grad_summaries.append(grad_hist_summary)
                grad_summaries.append(sparsity_summary)
        grad_summaries_merged = tf.summary.merge(grad_summaries)
        

        timestamp = str(int(time.time()))
        out_dir = os.path.abspath(os.path.join(os.path.curdir, "runs", timestamp))
        print("Writing to {}\n".format(out_dir))

        # Summaries for loss and accuracy
        loss_summary = tf.summary.scalar("loss", lstm.loss)
        acc_summary = tf.summary.scalar("accuracy", lstm.accuracy)

        # Train Summaries
        train_summary_op = tf.summary.merge([loss_summary, acc_summary])
        train_summary_dir = os.path.join(out_dir, "summaries", "train")
        train_summary_writer = tf.summary.FileWriter(train_summary_dir, sess.graph)

        # Dev summaries
        dev_summary_op = tf.summary.merge([loss_summary, acc_summary])
        dev_summary_dir = os.path.join(out_dir, "summaries", "dev")
        dev_summary_writer = tf.summary.FileWriter(dev_summary_dir, sess.graph)

        # Checkpoint directory. Tensorflow assumes this directory already exists so we need to create it
        checkpoint_dir = os.path.abspath(os.path.join(out_dir, "checkpoints"))
        checkpoint_prefix = os.path.join(checkpoint_dir, "model")
        if not os.path.exists(checkpoint_dir):
            os.makedirs(checkpoint_dir)
        saver = tf.train.Saver(tf.global_variables(), max_to_keep=num_checkpoints)

        # Write vocabulary
        vocab_processor.save(os.path.join(out_dir, "vocab"))

        # Initialize all variables
        sess.run(tf.global_variables_initializer())

        def train_step(x_batch, y_batch):
            """
            A single training step
            """
            feed_dict = {
              lstm.input_x: x_batch,
              lstm.input_y: y_batch,
              # lstm.dropout_keep_prob: dropout_keep_prob
            }

            _, step, summaries, loss, accuracy,output = sess.run(
                [train_op, global_step, train_summary_op, lstm.loss, lstm.accuracy, lstm.scores],
                feed_dict)
            time_str = datetime.datetime.now().isoformat()
            # print (output)
            print("{}: step {}, loss {:g}, acc {:g}".format(time_str, step, loss, accuracy))
            train_summary_writer.add_summary(summaries, step)

        def dev_step(x_batch, y_batch, writer=None):
            """
            Evaluates model on a dev set
            """
            feed_dict = {
              lstm.input_x: x_batch,
              lstm.input_y: y_batch,
            }
            step, summaries, loss, accuracy = sess.run(
                [global_step, dev_summary_op, lstm.loss, lstm.accuracy],
                feed_dict)
            time_str = datetime.datetime.now().isoformat()
            print("{}: step {}, loss {:g}, acc {:g}".format(time_str, step, loss, accuracy))
            if writer:
                writer.add_summary(summaries, step)

        # Generate batches
        batches = data_helpers.batch_iter(
            list(zip(x_train, y_train)), batch_size, num_epochs-1)
        batches_dev = data_helpers.batch_iter(
            list(zip(x_train, y_train)), batch_size, num_epochs-1)
        # Training loop. For each batch...
        for batch in batches:
            x_batch, y_batch = zip(*batch)
            train_step(x_batch, y_batch)
            current_step = tf.train.global_step(sess, global_step)
            if current_step % evaluate_every == 0:
                print("\nEvaluation:")
                # dev_step(x_dev, y_dev)

                print("")
            if current_step % checkpoint_every == 0:
                path = saver.save(sess, checkpoint_prefix, global_step=current_step)
                print("Saved model checkpoint to {}\n".format(path))

