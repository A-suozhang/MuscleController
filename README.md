# MuscleController

### Description
This is the implementation code for MuscleController. a user-friendly controller based on EMG with Deep Learning Methods
co_author: @rotom407 @GitZinc

![Image text](https://raw.githubusercontent.com/A-suozhang/MuscleController/master/Board.jpg)

### The Dataset
The dataset(dataset.npz) is at home folder, containing datas in numpy array(the last column being its label)

### The Neural Network Part
We adopt Deep Learning method when it comes to determine complex actions. We implemented the Text-CNN version and the LSTM version in Tensorflow(1.6.0) , both achieved good results. (based on git@github.com:dennybritz/cnn-text-classification-tf.git ) You could find it in MuscleController-TF folder.


We completed the PyTorch version of the Neural Network part afterwards, also could be seen at sEMGDemo-PyTorch.

### The Interaction Part
We implemented the Keyboard and Mouse interaction in Python with the package PyUserInput.

The final version of the fronter of MuscleController is written in C#, which could be found at MuscleController-C#


### Guide
1. Tensorflow
to run the Tensorflow version, get into the TF folder, then run 
>python train.py 

- for training the TextCNN 

- also for training the LSTM
> python train_new.py

- the Summary are in summaries/ and checkpoints are in checkpoint/

- To see the visualization of the Training, came to summaries/ folder then run

> tensorboard --logdir dev/

- run for testing a single sample

> python load_demo.py 

2. Pytorch
- Copy the dataset into the pytorch folder, then run (Only in Ubuntu since Pytorch don't support Windows now)
> python2 lstm.py




