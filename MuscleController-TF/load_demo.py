from text_cnn import TextCNN
import random
import tensorflow as tf 
import numpy as np 


ckpt = tf.train.get_checkpoint_state('./checkpoints/')
saver = tf.train.import_meta_graph(ckpt.model_checkpoint_path +'.meta')

with tf.Session() as sess:

	cnn = TextCNN(
            sequence_length=56,
            num_classes=2,
            vocab_size=18758,
            embedding_size=128,
            filter_sizes=[3,4,5],
            num_filters=128,
            l2_reg_lambda=0.0)
	sess.run(tf.global_variables_initializer())
	saver.restore(sess,ckpt.model_checkpoint_path) 
	for i in range (1000):
		a = np.random.rand(1,56)
		feed_dict = {cnn.input_x: a, cnn.dropout_keep_prob: 0.5}
		result = sess.run(cnn.scores, feed_dict)
		# print (result)

