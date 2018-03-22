namespace MuscleControllerFrontend
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(400D, 300D);
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnTrig = new System.Windows.Forms.Button();
            this.txtTest = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.tmrStart = new System.Windows.Forms.Timer(this.components);
            this.cht1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnStop = new System.Windows.Forms.Button();
            this.tmrUpdateChart = new System.Windows.Forms.Timer(this.components);
            this.cht2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grpCursor = new System.Windows.Forms.GroupBox();
            this.chkApply = new System.Windows.Forms.CheckBox();
            this.chtSim = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txtXYNow = new System.Windows.Forms.TextBox();
            this.txtXYBasis = new System.Windows.Forms.TextBox();
            this.btnXYReset = new System.Windows.Forms.Button();
            this.btnSetY = new System.Windows.Forms.Button();
            this.btnSetX = new System.Windows.Forms.Button();
            this.tmrUpdateCursor = new System.Windows.Forms.Timer(this.components);
            this.cht3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.cht1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cht2)).BeginInit();
            this.grpCursor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtSim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cht3)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(12, 14);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(44, 21);
            this.txtPort.TabIndex = 0;
            this.txtPort.Text = "COM4";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(62, 14);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(143, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnTrig
            // 
            this.btnTrig.Enabled = false;
            this.btnTrig.Location = new System.Drawing.Point(224, 14);
            this.btnTrig.Name = "btnTrig";
            this.btnTrig.Size = new System.Drawing.Size(75, 23);
            this.btnTrig.TabIndex = 3;
            this.btnTrig.Text = "Trigger";
            this.btnTrig.UseVisualStyleBackColor = true;
            this.btnTrig.Click += new System.EventHandler(this.btnTrig_Click);
            // 
            // txtTest
            // 
            this.txtTest.Location = new System.Drawing.Point(12, 41);
            this.txtTest.Name = "txtTest";
            this.txtTest.Size = new System.Drawing.Size(287, 21);
            this.txtTest.TabIndex = 4;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(12, 69);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tmrStart
            // 
            this.tmrStart.Interval = 500;
            this.tmrStart.Tick += new System.EventHandler(this.tmrStart_Tick);
            // 
            // cht1
            // 
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F);
            chartArea1.AxisY.Maximum = 30000D;
            chartArea1.AxisY.Minimum = -30000D;
            chartArea1.Name = "ChartArea1";
            this.cht1.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.cht1.Legends.Add(legend1);
            this.cht1.Location = new System.Drawing.Point(12, 98);
            this.cht1.Name = "cht1";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Color = System.Drawing.Color.Firebrick;
            series1.IsVisibleInLegend = false;
            series1.IsXValueIndexed = true;
            series1.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series1.Legend = "Legend1";
            series1.Name = "AcX";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Color = System.Drawing.Color.LimeGreen;
            series2.IsVisibleInLegend = false;
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend1";
            series2.Name = "AcY";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Color = System.Drawing.Color.RoyalBlue;
            series3.IsVisibleInLegend = false;
            series3.IsXValueIndexed = true;
            series3.Legend = "Legend1";
            series3.Name = "AcZ";
            this.cht1.Series.Add(series1);
            this.cht1.Series.Add(series2);
            this.cht1.Series.Add(series3);
            this.cht1.Size = new System.Drawing.Size(287, 91);
            this.cht1.TabIndex = 6;
            this.cht1.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "Accelerometer";
            this.cht1.Titles.Add(title1);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(93, 69);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tmrUpdateChart
            // 
            this.tmrUpdateChart.Interval = 10;
            this.tmrUpdateChart.Tick += new System.EventHandler(this.tmrUpdateChart_Tick);
            // 
            // cht2
            // 
            chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F);
            chartArea2.AxisY.Maximum = 30000D;
            chartArea2.AxisY.Minimum = -30000D;
            chartArea2.Name = "ChartArea1";
            this.cht2.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.cht2.Legends.Add(legend2);
            this.cht2.Location = new System.Drawing.Point(12, 195);
            this.cht2.Name = "cht2";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.Color = System.Drawing.Color.Firebrick;
            series4.IsVisibleInLegend = false;
            series4.IsXValueIndexed = true;
            series4.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series4.Legend = "Legend1";
            series4.Name = "GyX";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
            series4.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series5.Color = System.Drawing.Color.LimeGreen;
            series5.IsVisibleInLegend = false;
            series5.IsXValueIndexed = true;
            series5.Legend = "Legend1";
            series5.Name = "GyY";
            series6.BorderWidth = 2;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series6.Color = System.Drawing.Color.RoyalBlue;
            series6.IsVisibleInLegend = false;
            series6.IsXValueIndexed = true;
            series6.Legend = "Legend1";
            series6.Name = "GyZ";
            this.cht2.Series.Add(series4);
            this.cht2.Series.Add(series5);
            this.cht2.Series.Add(series6);
            this.cht2.Size = new System.Drawing.Size(287, 91);
            this.cht2.TabIndex = 9;
            this.cht2.Text = "chart1";
            title2.Name = "Title1";
            title2.Text = "Gyroscope (not used)";
            this.cht2.Titles.Add(title2);
            // 
            // grpCursor
            // 
            this.grpCursor.Controls.Add(this.chkApply);
            this.grpCursor.Controls.Add(this.chtSim);
            this.grpCursor.Controls.Add(this.txtXYNow);
            this.grpCursor.Controls.Add(this.txtXYBasis);
            this.grpCursor.Controls.Add(this.btnXYReset);
            this.grpCursor.Controls.Add(this.btnSetY);
            this.grpCursor.Controls.Add(this.btnSetX);
            this.grpCursor.Enabled = false;
            this.grpCursor.Location = new System.Drawing.Point(315, 14);
            this.grpCursor.Name = "grpCursor";
            this.grpCursor.Size = new System.Drawing.Size(251, 330);
            this.grpCursor.TabIndex = 11;
            this.grpCursor.TabStop = false;
            this.grpCursor.Text = "Cursor Movement";
            // 
            // chkApply
            // 
            this.chkApply.AutoSize = true;
            this.chkApply.Location = new System.Drawing.Point(7, 305);
            this.chkApply.Name = "chkApply";
            this.chkApply.Size = new System.Drawing.Size(54, 16);
            this.chkApply.TabIndex = 19;
            this.chkApply.Text = "Apply";
            this.chkApply.UseVisualStyleBackColor = true;
            this.chkApply.CheckedChanged += new System.EventHandler(this.chkApply_CheckedChanged);
            // 
            // chtSim
            // 
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisX.IsStartedFromZero = false;
            chartArea3.AxisX.MajorGrid.Enabled = false;
            chartArea3.AxisX.MajorTickMark.Enabled = false;
            chartArea3.AxisX.Maximum = 800D;
            chartArea3.AxisX.Minimum = 0D;
            chartArea3.AxisY.IsLabelAutoFit = false;
            chartArea3.AxisY.IsReversed = true;
            chartArea3.AxisY.IsStartedFromZero = false;
            chartArea3.AxisY.MajorGrid.Enabled = false;
            chartArea3.AxisY.MajorTickMark.Enabled = false;
            chartArea3.AxisY.Maximum = 600D;
            chartArea3.AxisY.Minimum = 0D;
            chartArea3.BorderColor = System.Drawing.Color.Transparent;
            chartArea3.Name = "ChartArea1";
            this.chtSim.ChartAreas.Add(chartArea3);
            legend3.Enabled = false;
            legend3.Name = "Legend1";
            this.chtSim.Legends.Add(legend3);
            this.chtSim.Location = new System.Drawing.Point(6, 127);
            this.chtSim.Name = "chtSim";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series7.Legend = "Legend1";
            series7.MarkerColor = System.Drawing.Color.Red;
            series7.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series7.Name = "Cursor";
            series7.Points.Add(dataPoint1);
            this.chtSim.Series.Add(series7);
            this.chtSim.Size = new System.Drawing.Size(239, 171);
            this.chtSim.TabIndex = 18;
            this.chtSim.Text = "chart1";
            // 
            // txtXYNow
            // 
            this.txtXYNow.Location = new System.Drawing.Point(6, 100);
            this.txtXYNow.Name = "txtXYNow";
            this.txtXYNow.Size = new System.Drawing.Size(239, 21);
            this.txtXYNow.TabIndex = 17;
            // 
            // txtXYBasis
            // 
            this.txtXYBasis.Location = new System.Drawing.Point(6, 49);
            this.txtXYBasis.Multiline = true;
            this.txtXYBasis.Name = "txtXYBasis";
            this.txtXYBasis.Size = new System.Drawing.Size(239, 45);
            this.txtXYBasis.TabIndex = 16;
            // 
            // btnXYReset
            // 
            this.btnXYReset.Location = new System.Drawing.Point(170, 20);
            this.btnXYReset.Name = "btnXYReset";
            this.btnXYReset.Size = new System.Drawing.Size(75, 23);
            this.btnXYReset.TabIndex = 14;
            this.btnXYReset.Text = "Reset";
            this.btnXYReset.UseVisualStyleBackColor = true;
            this.btnXYReset.Click += new System.EventHandler(this.btnXYReset_Click);
            // 
            // btnSetY
            // 
            this.btnSetY.Location = new System.Drawing.Point(88, 20);
            this.btnSetY.Name = "btnSetY";
            this.btnSetY.Size = new System.Drawing.Size(75, 23);
            this.btnSetY.TabIndex = 13;
            this.btnSetY.Text = "Set Ybase";
            this.btnSetY.UseVisualStyleBackColor = true;
            this.btnSetY.Click += new System.EventHandler(this.btnSetY_Click);
            // 
            // btnSetX
            // 
            this.btnSetX.Location = new System.Drawing.Point(6, 20);
            this.btnSetX.Name = "btnSetX";
            this.btnSetX.Size = new System.Drawing.Size(75, 23);
            this.btnSetX.TabIndex = 12;
            this.btnSetX.Text = "Set Xbase";
            this.btnSetX.UseVisualStyleBackColor = true;
            this.btnSetX.Click += new System.EventHandler(this.btnSetX_Click);
            // 
            // tmrUpdateCursor
            // 
            this.tmrUpdateCursor.Interval = 30;
            this.tmrUpdateCursor.Tick += new System.EventHandler(this.tmrUpdateCursor_Tick);
            // 
            // cht3
            // 
            chartArea4.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea4.AxisX.IsLabelAutoFit = false;
            chartArea4.AxisX.LabelStyle.Enabled = false;
            chartArea4.AxisY.IsLabelAutoFit = false;
            chartArea4.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F);
            chartArea4.AxisY.Maximum = 30000D;
            chartArea4.AxisY.Minimum = -30000D;
            chartArea4.Name = "ChartArea1";
            this.cht3.ChartAreas.Add(chartArea4);
            legend4.Enabled = false;
            legend4.Name = "Legend1";
            this.cht3.Legends.Add(legend4);
            this.cht3.Location = new System.Drawing.Point(12, 292);
            this.cht3.Name = "cht3";
            series8.BorderWidth = 2;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series8.Color = System.Drawing.Color.DodgerBlue;
            series8.IsVisibleInLegend = false;
            series8.IsXValueIndexed = true;
            series8.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series8.Legend = "Legend1";
            series8.Name = "Ch1";
            series8.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
            series8.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
            series9.BorderWidth = 2;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series9.Color = System.Drawing.Color.Gold;
            series9.IsVisibleInLegend = false;
            series9.IsXValueIndexed = true;
            series9.Legend = "Legend1";
            series9.Name = "Ch2";
            series10.BorderWidth = 2;
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series10.Color = System.Drawing.Color.SpringGreen;
            series10.IsVisibleInLegend = false;
            series10.IsXValueIndexed = true;
            series10.Legend = "Legend1";
            series10.Name = "Ch3";
            series11.BorderWidth = 2;
            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series11.Color = System.Drawing.Color.OrangeRed;
            series11.IsVisibleInLegend = false;
            series11.IsXValueIndexed = true;
            series11.Legend = "Legend1";
            series11.Name = "Ch4";
            this.cht3.Series.Add(series8);
            this.cht3.Series.Add(series9);
            this.cht3.Series.Add(series10);
            this.cht3.Series.Add(series11);
            this.cht3.Size = new System.Drawing.Size(287, 91);
            this.cht3.TabIndex = 12;
            title3.Name = "Title1";
            title3.Text = "EMG Envelope (WIP)";
            this.cht3.Titles.Add(title3);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 395);
            this.Controls.Add(this.cht3);
            this.Controls.Add(this.grpCursor);
            this.Controls.Add(this.cht2);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.cht1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtTest);
            this.Controls.Add(this.btnTrig);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "MuscleController Frontend";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cht1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cht2)).EndInit();
            this.grpCursor.ResumeLayout(false);
            this.grpCursor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtSim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cht3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnTrig;
        private System.Windows.Forms.TextBox txtTest;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer tmrStart;
        private System.Windows.Forms.DataVisualization.Charting.Chart cht1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer tmrUpdateChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart cht2;
        private System.Windows.Forms.GroupBox grpCursor;
        private System.Windows.Forms.Button btnSetX;
        private System.Windows.Forms.Button btnXYReset;
        private System.Windows.Forms.Button btnSetY;
        private System.Windows.Forms.TextBox txtXYNow;
        private System.Windows.Forms.TextBox txtXYBasis;
        private System.Windows.Forms.Timer tmrUpdateCursor;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtSim;
        private System.Windows.Forms.DataVisualization.Charting.Chart cht3;
        private System.Windows.Forms.CheckBox chkApply;
    }
}

