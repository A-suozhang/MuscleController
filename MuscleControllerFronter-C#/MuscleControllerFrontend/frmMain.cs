using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace MuscleControllerFrontend {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }

        //when the form is loaded
        private void frmMain_Load(object sender, EventArgs e) {
            Globals.sthread.Start();    //start the serial worker thread
            UpdateSerialState(Serialstate.Closed);    //set the serial state to closed
            UpdateCursorXYZbaseTextbox();
            for (int i = 0; i < 3; i++) {   //set the cutoff freqs of filters
                Globals.chfilters[i] = new Filters(0.995, 0.5, 0.7, 1000);
                Globals.filters[i] = new Filters(0.995, 0.5, 0.7, 1000);
            }
            //set Y ranges of charts
            cht1.ChartAreas[0].Axes[1].Maximum = 3000;
            cht1.ChartAreas[0].Axes[1].Minimum = -3000;
            cht2.ChartAreas[0].Axes[1].Maximum = 10000;
            cht2.ChartAreas[0].Axes[1].Minimum = -10000;
            cht3.ChartAreas[0].Axes[1].Maximum = 1000;
            cht3.ChartAreas[0].Axes[1].Minimum = 0;
        }

        //when the form is closed
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e) {
            Globals.sworker.Kill(); //kill the serial worker process
        }

        //when Open button is clicked
        private void btnOpen_Click(object sender, EventArgs e) {
            try {
                Globals.sworker.SetPortOpened(true, txtPort.Text);  //tell the serial worker to open the serial port
                btnOpen.Enabled = false;
                tmrStart.Enabled = true;    //wait for some time before doing something next
            } catch {
                MessageBox.Show("Error opening serial port.");
                UpdateSerialState(Serialstate.Closed);
            }
        }
        private void tmrStart_Tick(object sender, EventArgs e) {
            UpdateSerialState(Serialstate.Single);  //enable the buttons
            tmrStart.Enabled = false;
        }

        //update serial state and enable/disable the buttons
        private void UpdateSerialState(Serialstate state) {
            Globals.state = state;
            switch (state) {
                case Serialstate.Closed:
                    btnClose.Enabled = false;
                    btnOpen.Enabled = true;
                    btnTrig.Enabled = false;
                    btnStart.Enabled = false;
                    btnStop.Enabled = false;
                    grpCursor.Enabled = false;
                    break;
                case Serialstate.Single:
                    btnClose.Enabled = true;
                    btnOpen.Enabled = false;
                    btnTrig.Enabled = true;
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                    grpCursor.Enabled = false;
                    break;
                case Serialstate.Continuous:
                    btnClose.Enabled = false;
                    btnOpen.Enabled = false;
                    btnTrig.Enabled = false;
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    grpCursor.Enabled = true;
                    break;
            }
        }

        //when close button is clicked
        private void btnClose_Click(object sender, EventArgs e) {
            try {
                Globals.sworker.SetPortOpened(false, "");   //close the serial port
                UpdateSerialState(Serialstate.Closed);
            } catch {
                MessageBox.Show("Error closing serial port.");
            }
        }

        //when trigger button is clicked
        private void btnTrig_Click(object sender, EventArgs e) {
            try {
                Globals.sworker.SetCount(1);    //set trigger count to 1
                Thread.Sleep(50);   //wait data to be received
                lock (Globals.dbuf.buf) {
                    string str = "";
                    for (int i = 0; i < 10; i++) {
                        str += (Globals.dbuf.buf.First().Data[i]) + " ";
                    }
                    Globals.dbuf.buf.Clear();
                    txtTest.Text = str; //display the result
                }
                lock (Globals.dbuf.chbuf) {
                    Globals.dbuf.chbuf.Clear();
                }
            } catch {
                MessageBox.Show("Error writing serial port.");
            }
        }

        //when start button is clicked
        private void btnStart_Click(object sender, EventArgs e) {
            if (Globals.state != Serialstate.Closed) {
                Globals.sworker.SetCount(-1);   //set serial worker status to continuous
                tmrUpdateChart.Enabled = true;
                tmrUpdateCursor.Enabled = true;
                UpdateSerialState(Serialstate.Continuous);
            }
        }

        //when stop button is clicked
        private void btnStop_Click(object sender, EventArgs e) {
            if (Globals.state != Serialstate.Closed) {
                Globals.sworker.SetCount(0);    //stop the serial worker
                tmrUpdateChart.Enabled = false;
                tmrUpdateCursor.Enabled = false;
                UpdateSerialState(Serialstate.Single);
            }
        }

        //when update chart timer is triggered
        private void tmrUpdateChart_Tick(object sender, EventArgs e) {
            lock (Globals.dbuf.chbuf) { //lock the data buffer when accessing it to prevent bad things
                foreach (var dat in Globals.dbuf.chbuf) {   //read all data records in the buffer
                    foreach (var name in new string[] { "AcX", "AcY", "AcZ" }) {
                        int index = name == "AcX" ? 0 : (name == "AcY" ? 1 : 2);
                        Globals.chfilters[index].Feed(dat.Data[index]);
                        cht1.Series[name].Points.SuspendUpdates();
                        cht1.Series[name].Points.Add(Globals.chfilters[index].Smooth);  //add points to charts
                        if (cht1.Series[name].Points.Count > 60)
                            cht1.Series[name].Points.RemoveAt(0);   //if a chart has more than 60 points, remove the first point
                        cht1.Series[name].Points.ResumeUpdates();
                    }
                    foreach (var name in new string[] { "GyX", "GyY", "GyZ" }) {
                        int index = name == "GyX" ? 3 : (name == "GyY" ? 4 : 5);
                        cht2.Series[name].Points.SuspendUpdates();
                        cht2.Series[name].Points.Add(dat.Data[index]);
                        if (cht2.Series[name].Points.Count > 60)
                            cht2.Series[name].Points.RemoveAt(0);
                        cht2.Series[name].Points.ResumeUpdates();
                    }
                    foreach (var name in new string[] { "Ch1", "Ch2", "Ch3", "Ch4" }) {
                        int index = name == "Ch1" ? 6 : (name == "Ch2" ? 7 : (name == "Ch3" ? 8 : 9));
                        cht3.Series[name].Points.SuspendUpdates();
                        cht3.Series[name].Points.Add(dat.Data[index]);
                        if (cht3.Series[name].Points.Count > 60)
                            cht3.Series[name].Points.RemoveAt(0);
                        cht3.Series[name].Points.ResumeUpdates();
                    }
                }
                Globals.dbuf.chbuf.Clear(); //clear the buffer
            }
        }

        //when update cursor timer is triggered
        private void tmrUpdateCursor_Tick(object sender, EventArgs e) {
            lock (Globals.dbuf.buf) { //lock the data buffer when accessing it to prevent bad things
                short[] lastdat = new short[10];
                for (int i = 0; i < 10; i++) {
                    lastdat[i] = Globals.lastrec.Data[i];
                }
                foreach (var dat in Globals.dbuf.buf) { //read all data records in the buffer
                    if (dat.Success) {  //if the data record is marked with 'success'
                        foreach (var name in new string[] { "AcX", "AcY", "AcZ" }) {
                            int index = name == "AcX" ? 0 : (name == "AcY" ? 1 : 2);
                            Globals.filters[index].Feed(dat.Data[index]);   //feed the filters with AcX AcY AcZ
                        }
                        for (int i = 0; i < 3; i++) {
                            lastdat[i] = (short)Globals.filters[i].Smooth;  //get the output of filters
                        }
                        for (int i = 3; i < 10; i++) {
                            lastdat[i] = dat.Data[i];   //store other data (GyX, GyY, ...)
                        }
                    }
                }
                for (int i = 0; i < 10; i++) {
                    Globals.lastrec.Data[i] = lastdat[i];
                }
                Globals.dbuf.buf.Clear();   //clear the buffer
            }
            //calculate XY speeds from accelerometer data
            Vector3D nowvec = new Vector3D(Globals.lastrec.Data[0], Globals.lastrec.Data[1], Globals.lastrec.Data[2]);
            nowvec *= Globals.cursordat.Speed;
            Vector3D decompvec = nowvec.Decompose(Globals.cursordat.Xbase, Globals.cursordat.Ybase, Globals.cursordat.Zbase);
            double tempxsp = decompvec.X, tempysp = decompvec.Y;
            //double tempxsp = -Globals.lastrec.data[5] * Globals.cursordat.speed/3, tempysp = -Globals.lastrec.data[4] * Globals.cursordat.speed/3;
            double norm = Math.Sqrt(tempxsp * tempxsp + tempysp * tempysp);
            double deadzone = 2.5;
            //apply deadzone
            if (norm > deadzone) {
                tempxsp *= Math.Pow((norm - deadzone) / 1.5, 1.3) / norm;
                tempysp *= Math.Pow((norm - deadzone) / 1.5, 1.3) / norm;
            } else {
                tempysp = tempxsp = 0.0;
            }
            //update XY speeds and record the difference
            Globals.cursordat.Xsp = tempxsp;
            Globals.cursordat.Ysp = tempysp;
            double origx = Globals.cursordat.X, origy = Globals.cursordat.Y;
            Globals.cursordat.UpdateXY();
            double nowx = Globals.cursordat.X, nowy = Globals.cursordat.Y;
            /*
            Globals.cursordat.X = 400 + 20*tempxsp;
            Globals.cursordat.Y = 300 + 20*tempysp;*/
            //truncate the floating point numbers into integers, recording the remaining fractional parts
            int outx, outy;
            outx = (int)Math.Truncate(nowx - origx + Globals.cursordat.Xremaining);
            outy = (int)Math.Truncate(nowy - origy + Globals.cursordat.Yremaining);
            UpdateCursorXYSpeedTextbox(decompvec.X, decompvec.Y, outx, outy);
            Globals.cursordat.Xremaining = nowx - origx + Globals.cursordat.Xremaining - outx;
            Globals.cursordat.Yremaining = nowy - origy + Globals.cursordat.Yremaining - outy;
            UpdateSimChart();   //update cursor simulation chart
            if (Globals.cursordat.Active)
                MoveCursor(outx, outy); //call Windows API to move the cursor if enabled
        }

        //when reset button is clicked
        private void btnXYReset_Click(object sender, EventArgs e) {
            //reset the XYZ bases
            Globals.cursordat.Xbase.X = 0.0;
            Globals.cursordat.Xbase.Y = -1.0;
            Globals.cursordat.Xbase.Z = 0.0;
            Globals.cursordat.Ybase.X = 0.0;
            Globals.cursordat.Ybase.Y = 0.0;
            Globals.cursordat.Ybase.Z = 1.0;
            Globals.cursordat.Zbase.X = -1.0;
            Globals.cursordat.Zbase.Y = 0.0;
            Globals.cursordat.Zbase.Z = 0.0;
            UpdateCursorXYZbaseTextbox();
        }

        //call Windows API to move the cursor
        private void MoveCursor(int dx, int dy) {
            Cursor = new System.Windows.Forms.Cursor(System.Windows.Forms.Cursor.Current.Handle);
            System.Windows.Forms.Cursor.Position = new Point(System.Windows.Forms.Cursor.Position.X + dx, System.Windows.Forms.Cursor.Position.Y + dy);
        }

        private void UpdateCursorXYZbaseTextbox() {
            txtXYBasis.Text = "Xbase = " + Globals.cursordat.Xbase.ToString() + "\r\nYbase = " + Globals.cursordat.Ybase.ToString() + "\r\nZbase = " + Globals.cursordat.Zbase.ToString();
        }

        private void UpdateCursorXYSpeedTextbox(double xsp, double ysp, int outx, int outy) {
            txtXYNow.Text = string.Format("Xsp = {0:+0.00;-0.00} Ysp = {1:+0.00;-0.00} dX = {2} dY = {3}", xsp, ysp, outx, outy);
        }

        private void UpdateSimChart() {
            chtSim.Series["Cursor"].Points[0].XValue = Globals.cursordat.X;
            chtSim.Series["Cursor"].Points[0].YValues[0] = Globals.cursordat.Y;
        }

        //when setxbase button is clicked
        private void btnSetX_Click(object sender, EventArgs e) {
            //calculate x base and z base (Zbase=Xbase x Ybase)
            Globals.cursordat.Xbase.X = Globals.lastrec.Data[0];
            Globals.cursordat.Xbase.Y = Globals.lastrec.Data[1];
            Globals.cursordat.Xbase.Z = Globals.lastrec.Data[2];
            Globals.cursordat.Xbase.Normalize();
            Globals.cursordat.Zbase=Globals.cursordat.Xbase.Cross(Globals.cursordat.Ybase);
            UpdateCursorXYZbaseTextbox();
        }

        //when setxbase button is clicked
        private void btnSetY_Click(object sender, EventArgs e) {
            //calculate y base and z base (Zbase=Xbase x Ybase)
            Globals.cursordat.Ybase.X = Globals.lastrec.Data[0];
            Globals.cursordat.Ybase.Y = Globals.lastrec.Data[1];
            Globals.cursordat.Ybase.Z = Globals.lastrec.Data[2];
            Globals.cursordat.Ybase.Normalize();
            Globals.cursordat.Zbase=Globals.cursordat.Xbase.Cross(Globals.cursordat.Ybase);
            UpdateCursorXYZbaseTextbox(); 
        }

        //when apply checkbox is changed
        private void chkApply_CheckedChanged(object sender, EventArgs e) {
            Globals.cursordat.Active = chkApply.Checked;    //update the status of cursor movement enabled setting
        }
    }
}
