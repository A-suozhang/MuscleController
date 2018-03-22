using System.Threading;

namespace MuscleControllerFrontend {
    //static class storing global objects
    public static class Globals {
        public static Serialstate state = Serialstate.Closed;
        public static Filters[] filters = new Filters[3];   //3 filters (for AcX AcY AcZ) used for cursor movements
        public static Filters[] chfilters = new Filters[3]; //3 filters (for AcX AcY AcZ) used for charts
        public static volatile byte timestamp = 0;
        public static volatile DataBuffer dbuf = new DataBuffer();  //data buffer used by serial worker
        public static SerialWorker sworker = new SerialWorker();
        public static Thread sthread = new Thread(sworker.DoWork);
        public static CursorData cursordat = new CursorData();  //cursor data
        public static DataRecord lastrec = new DataRecord(0, true); //last (current) record of sensors
    }

    //storing the position, velocity and other properties of the cursor
    public class CursorData {
        //x, y, z base vectors of the user-defined coordinate system, zbase is calculated as the cross product of xbase and ybase
        private Vector3D xbase = new Vector3D(0, -1, 0);
        private Vector3D ybase = new Vector3D(0, 0, 1);
        private Vector3D zbase = new Vector3D(-1, 0, 0);
        private double speed = 0.005;   //cursor movement speed
        private double x = 400.0;   //cursor position
        private double y = 300.0;
        private double xsp = 0.0;   //cursor speed
        private double ysp = 0.0;
        private double xremaining = 0.0;    //remaining fractional part after truncation (when passing x y values to Windows API) of x y values
        private double yremaining = 0.0;
        private bool active = false;

        //encapsulations
        public Vector3D Xbase { get => xbase; set => xbase = value; }
        public Vector3D Ybase { get => ybase; set => ybase = value; }
        public Vector3D Zbase { get => zbase; set => zbase = value; }
        public double Speed { get => speed; set => speed = value; }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Xsp { get => xsp; set => xsp = value; }
        public double Ysp { get => ysp; set => ysp = value; }
        public double Xremaining { get => xremaining; set => xremaining = value; }
        public double Yremaining { get => yremaining; set => yremaining = value; }
        public bool Active { get => active; set => active = value; }

        //update cursor position with speed values and clamp the position ranges
        public void UpdateXY(int maxX, int maxY) {
            X += Xsp; Y += Ysp;
            if (X > maxX) X = maxX;
            if (Y > maxY) Y = maxY;
            if (X < 0) X = 0;
            if (Y < 0) Y = 0;
        }

        //update cursor position with speed values without clamp the position ranges
        public void UpdateXY() {
            X += Xsp; Y += Ysp;
        }
    }

    //filters class
    public class Filters {
        private LowpassFilter trend;    //low pass filter with low cutoff frequency to get the trend from data
        private LowpassFilter denoise;  //low pass filter with high cutoff freq to remove noise from data preserving sudden changes
        private LowpassFilterCutoff tilt; //low pass filter with moderate cutoff freq and peak cutoff to remove sudden changes from data
        private double smooth;  //=tilt-trend (effectively a band pass filter), represents tilt angle of the sensor
        private double sudden;  //=denoise-tilt (effectively a band pass filter), represents sudden accelerations of the sensor (currently not used)
        private bool reset = true;  //whether to reset the filters' memory in the next update

        //encapsulations
        public LowpassFilter Trend { get => trend; set => trend = value; }
        public LowpassFilter Denoise { get => denoise; set => denoise = value; }
        public LowpassFilterCutoff Tilt { get => tilt; set => tilt = value; }
        public double Smooth { get => smooth; set => smooth = value; }
        public double Sudden { get => sudden; set => sudden = value; }
        public bool Reset { get => reset; set => reset = value; }

        //constructor
        public Filters(double trendcutoff, double denoisecutoff, double tiltcutoff, double tiltpeakcutoff) {
            Trend = new LowpassFilter(trendcutoff);
            Denoise = new LowpassFilter(denoisecutoff);
            Tilt = new LowpassFilterCutoff(tiltcutoff, tiltpeakcutoff);
        }

        //feed the filters with data input
        public void Feed(double input) {
            if (Reset) {
                //reset filter memory
                Trend.output = input;
                Denoise.output = input;
                Tilt.output = input;
                Reset = false;
            }
            Trend.Feed(input); Denoise.Feed(input); Tilt.Feed(input);
            Smooth = Tilt.output - Trend.output;
            Sudden = Denoise.output - Tilt.output;
        }
    }
}
