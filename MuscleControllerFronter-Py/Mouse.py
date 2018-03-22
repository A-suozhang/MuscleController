
from pymouse import PyMouse
from eval_pre import sequencePred
import time
import serial
import win32api
import win32con
import win32gui


'''Keyboard GUI'''
from PyQt5.QtWidgets import *
import sys
from PyQt5.QtGui import QColor

class keyboard(QWidget):
    """docstring for keyboard"""
    def __init__(self):
        super().__init__()
        self.initUI()
    def initUI(self):
        grid = QGridLayout()
        self.setLayout(grid)

        self.setGeometry(500,500,500,550)
        grid.setSpacing(10)
        self.setWindowTitle('keyboard siulator')
        self.tb = QTextBrowser(self)
        # self.lc = QLCDNumber()

        grid.addWidget(self.tb,0,0,1,0)
        grid.setSpacing(10)

        names = ['Q', 'W', 'E','R', 'T' ,'Y' ,'U' ,'I' ,'O' ,'P',
                'A', 'S', 'D', 'F','G','H','J','K','L',':',
                'Z', 'X', 'C', 'V', 'B','N','M',',','.','?',
                ]
        #   self.key_a = QLabel('A',self)
        #   self.key_a.move(20,20)
        # positions = [(i,j) for i in range(4,7) for j in range(4,10)]
        # for position, name in zip(positions, names):
        #   if name == '':
        #       continue
        # button = QPushButton(name)
        # grid.addWidget(button, *position)
        # button.clicked.connect(self.showDialog)
        # self.show()
        positions = [(i,j) for i in range(1,4) for j in range(0,10)]
        for position, name in zip(positions, names):
            if name == '':
                continue
            button = QPushButton(name)
            grid.addWidget(button, *position)
            button.clicked.connect(self.Cli)

        self.show()


    def Cli(self):
        sender = self.sender().text()
    #   sender = self.sender().text()
    #   print(text)
        self.tb.insertPlainText(sender)

    def ShowKeyBoard():
        app = QApplication(sys.argv)
        ex = keyboard()
        app.setStyleSheet()
        sys.exit(app.exec_())


'''The Mouse'''
m = PyMouse()



def leftclick(boo ,boo2, muscleleftshoulder, m):
    summit = 20
    valley = 6

    if ((boo == 0) and (muscleleftshoulder>summit)):
        (tempx,tempy) = m.position()
        #Need to implement doubleclick and hold   m.press()  m.release()
        #We are trying to use a gated structure
        boo = 1
        win32api.mouse_event(win32con.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
        print ("Presssing Left")

    else:
        pass

    if ((boo ==1) and (muscleleftshoulder<valley)):
        (tempx, tempy) = m.position()
        win32api.mouse_event(win32con.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
        print("Releasing Left")
        boo = 0

    return boo

def rightclick(boo2, musclerightshoulder, m):
    summit = 10

    if (boo2 == 0 and musclerightshoulder>summit):
        (tempx,tempy) = m.position()
        win32api.mouse_event(win32con.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
        boo2 = 1
        print ("Pressing right")

    if (boo2 ==1 and musclerightshoulder<=summit):
        (tempx, tempy) = m.position()
        win32api.mouse_event(win32con.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
        boo2 = 0
        print("releasing right")

    return boo2

def doubleclick(boo, boo2, lastboo, lastboo2, m):
    if (boo == 1 and lastboo2 ==0 and boo2 ==1):
        (tempx, tempy) = m.position()
        m.click(tempx, tempy, 1)
        m.click(tempx, tempy, 1)
        print ("Double Clickin'")
    elif (boo2 ==1 and lastboo ==0 and boo ==1):
        (tempx, tempy) = m.position()
        m.click(tempx, tempy, 1)
        m.click(tempx, tempy, 1)
        print ("DoubleClickin'")





'''移动鼠标（由于win32api提供的鼠标移动只能移动整数像素，所以要做许多处理）'''
def movemouse(a, b, speed,m):
        (x, y) = m.position()  #预先读取出目前鼠标的位置作为基准
        x = int(x)
        y = int(y)
        b = int(b*speed)
        a = int(a*speed)
        y = y - b   #与真实坐标对应
        x = x - a
        m.move(x,y)

def RollScreen(muscleback,x, speed):
    threshold  = 32
    if (muscleback>=0):
        if (x>=0) :
            m.scroll(1*speed)
        else:
            m.scroll((-1)*speed)

def process_data(b):
    #the input is  b a byte string
    muscleleftshoulder = b[0]
    musclerightshoulder = b[1]
    musclefront = b[2]
    muscleback = b[3]
    gyrox = b[4]
    gyroy = b[5]
    print (gyrox)

'''将串口给到的0~255的16进制byte数据转化为-128~127的位置数据'''
def XYtransform(a):
    if (a>127):
        a = (-1)*(256-a)
    else:
        pass
    if (abs(a)<10):  #削减过小的数据
        a = 0
    return a

'''将XYTransform后的数据归10化'''
def double2int(a,b):
    speedx = a/8
    speedy = b/8
    speed = (speedx**2 + speedy**2)**0.7  #由输入数据的大小计算移动鼠标速度
    if (a+b != 0):
        a1 = a/(abs(a)+abs(b))
        b1 = b/(abs(a)+abs(b))
    else:
        a1 = 0  #防止除0
        b1 = 0
    return round(a1*5), round(b1*5),speed


def processMuscle(lastmuscle, muscle):
    muscle = 0.9*lastmuscle + 0.1*muscle
    return abs(muscle)


'''只在几个通道都>阈值时候才记录'''
def saveAndClassifySeq(l, seqMat):
    if (l[0] < acitivateTreshold or l[1] < acitivateTreshold):
        print("The Confidence Value is: ", sequencePred(saveSeq))
        return seqMat
    if (l[0] > acitivateTreshold):
        seqMat[0].append(l[0])
    if (l[1] > acitivateTreshold):
        seqMat[1].append(l[1])
        return seqMat


'''循环主函数'''
def loop():
    ser  =serial.Serial(port = "COM3", baudrate = 115200) #打开串口并读取数据
    #ser.open()
    ser.flush()   #每次清空串口区
    boo = 0
    boo2 = 0
    lastmuscleleftshoulder = 0
    lastmusclerightshoulder = 0
    seqMat = [[],[]]
    while (True):
        a = ser.readline() #读一行数据
        #print (a)
        #print (len(a))
        if (len(a)>=5):  #判定数据长度，防止读到空数据抛出异常
            X = XYtransform(a[0])
            Y = XYtransform(a[1])
            muscleleftshoulder = a[2]
            #thresholdadjust_left = processMuscle(lastmuscleleftshoulder, muscleleftshoulder)
            #muscleleftshoulder = muscleleftshoulder - thresholdadjust_left
            #lastmuscleleftshoulder = thresholdadjust_left
            musclerightshoulder = a[3]
            #thresholdadjust_rihgt = processMuscle(lastmusclerightshoulder, musclerightshoulder)
            #musclerightshoulder = musclerightshoulder - thresholdadjust_rihgt
            #lastmusclerightshoulder = thresholdadjust_rihgt
            (x,y,s) = double2int(X,Y)
            print(x,y,s,muscleleftshoulder,musclerightshoulder)
            l = [X,Y]
            seqMat = saveAndClassifySeq(l, seqMat)
            ShowKeyBoard()
            movemouse(x,y,s,m)
            lastboo = boo
            lastboo2 = boo2
            #boo = leftclick(boo, boo2, muscleleftshoulder,m)
            #boo2 = rightclick(boo2, musclerightshoulder,m)
            #doubleclick(boo, boo2, lastboo, lastboo2, m)

            #RollScreen(muscleback = 0,speed = 50)



loop()

