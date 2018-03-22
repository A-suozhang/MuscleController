import tensorflow as tf 
import numpy as np 

class myLSTM(object):

	def __init__(self, sequence_length, n_classes, n_input, n_hidden_units, batch_size):
		self.input_x = tf.placeholder(tf.float32, [None, sequence_length])
		self.input_y = tf.placeholder(tf.float32, [None, n_classes])

		# l = self.input_x.get_shape().as_list()
		# print ("L[0]!", l)
		# if (batch_size != l[0]):
			# batch_size = l[0]
		

		weights = {
		'in':tf.Variable(tf.truncated_normal([n_input, n_hidden_units], mean = 1.0), name = "in_W"),
		'out':tf.Variable(tf.truncated_normal([n_hidden_units, n_classes], mean = 1.0, name = "out_W"))
		}

		biases = {
		'in':tf.Variable(tf.constant(1.0, shape = [n_hidden_units, ])),
		'out':tf.Variable(tf.constant(1.0, shape = [n_classes, ]))
		}

		#[batch_size, sequence_length]
		self.x_expanded = tf.expand_dims(self.input_x, -1)
		#[batch_size, sequence_length, n_input]
		self.X = tf.reshape(self.x_expanded, [-1, n_input])
		self.X_in = tf.matmul(self.X, weights['in'])+biases['in']
		#[batch_size, seq_len, hidden]
		self.X_in = tf.reshape(self.X_in, [-1, sequence_length, n_hidden_units])

		lstm_cell = tf.contrib.rnn.BasicLSTMCell(n_hidden_units, forget_bias = 0.1)
		mlstm_cell = tf.contrib.rnn.MultiRNNCell([lstm_cell]*2)
		init_state = mlstm_cell.zero_state(batch_size , dtype = tf.float32)

		self.outputs, final_state = tf.nn.dynamic_rnn(mlstm_cell, self.X_in,initial_state = init_state, time_major = False)
		self.scores = tf.nn.xw_plus_b(final_state[-1][1], weights['out'], biases['out'], name = "scores")
		self.predictions = tf.argmax(self.scores, 1, name="predictions")


		with tf.name_scope('loss'):
			self.loss = tf.reduce_mean(tf.nn.softmax_cross_entropy_with_logits(logits = self.scores, labels = self.input_y))

		with tf.name_scope('Accuracy'):
			correct_predictions = tf.equal(self.predictions, tf.argmax(self.input_y, 1))
			self.accuracy = tf.reduce_mean(tf.cast(correct_predictions, "float"), name="accuracy")



