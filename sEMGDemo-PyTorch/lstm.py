#coding:utf-8

import torch
import torch.nn as nn
from torch.autograd import *
import torch.utils.data as Data 
import torch.optim as optim
import torch.nn.functional as F 
import matplotlib.pyplot as plt 
import numpy as np 
from numpy import *
import scipy.io as sio
import cPickle as pickle
import matplotlib.pyplot as plt

'''Load Dataset and generate batch'''
data = np.load('dataset.npy')
label = data[:,3000]
datin = data[:,0:3000]
torch_dataset = Data.TensorDataset(data_tensor =torch.FloatTensor(datin), target_tensor =torch.FloatTensor(label))
loader = Data.DataLoader(  
    dataset=torch_dataset,  
    batch_size=16, # 批大小  
    shuffle=True, # 是否随机打乱顺序  
    num_workers=4, # 多线程读取数据的线程数  
    )  

class LSTM(nn.Module):
	def __init__(self):
		super(LSTM, self).__init__()
		
		self.lstm = nn.LSTM(
			input_size = 1,
			hidden_size = 10,
			num_layers = 1,
			batch_first = True,)

		self.out = nn.Linear(10, 1)

	def forward(self, x):
		# x shape (batch, time_step, input_size)
		# r_out shape (batch, time_step, output_size)
		# h_n shape (n_layers, batch, hidden_size)  
		# h_c shape (n_layers, batch, hidden_size)
		r_out, (h_n, h_c) = self.lstm(x, None)
		out = self.out(r_out[:,-1,:])
		return out

lstm = LSTM()
lstm = lstm.float()
optimizer = torch.optim.Adam(lstm.parameters(), lr = 0.005)
loss_function = nn.MSELoss()
l = []
for epoch in range (5):
	for step, (x,y) in enumerate(loader):
		x = Variable(x.view(-1,3000,1))
		y = Variable(y)
		y = y.float()

		output = lstm(x)

		loss = loss_function(output, y)
		print (loss.data[0])
		l.append(loss.data[0])
		optimizer.zero_grad()
		loss.backward()
		optimizer.step()


print (l)
plt.plot(l)
plt.show()		

