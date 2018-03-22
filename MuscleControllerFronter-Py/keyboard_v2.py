#coding=utf-8

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
		#	self.key_a = QLabel('A',self)
		#   self.key_a.move(20,20)
		# positions = [(i,j) for i in range(4,7) for j in range(4,10)]
		# for position, name in zip(positions, names):
		# 	if name == '':
		# 		continue
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
	#	sender = self.sender().text()
	#	print(text)
		self.tb.insertPlainText(sender)

if __name__ == '__main__':
	app = QApplication(sys.argv)
	ex = keyboard()
	app.setStyleSheet('''
    QPushButton{
        background-color: #0f0 ;
        height:20px;
        border-style: outset;
        border-width: 2px;
        border-radius: 10px;
        border-color: beige;
        font: bold 14px;
        min-width: 3em;
        padding: 6px;
    }
    QPushButton:hover {
    background-color: yellow;
    border-style: inset;
    }
    QPushButton:pressed {
    background-color: rgb(224, 0, 0);
    border-style: inset;
    }
    QPushButton#cancel{
        background-color: red ;
    }
    ''')
	sys.exit(app.exec_())