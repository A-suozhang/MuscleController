using System;

namespace MuscleControllerFrontend {
    //3D vector class
    public class Vector3D {
        private double x;
        private double y;
        private double z;

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }

        //consturctor
        public Vector3D(double xp, double yp, double zp) {
            x = xp; y = yp; z = zp;
        }
        //dot product
        public double Dot(Vector3D vec) {
            return x * vec.x + y * vec.y + z * vec.z;
        }
        //cross product
        public Vector3D Cross(Vector3D vec) {
            return new Vector3D(-vec.y * z + vec.z * y, vec.x * z - vec.z * x, -vec.x * y + vec.y * x);
        }
        //decompose the vector along x, y, z bases
        public Vector3D Decompose(Vector3D ve1, Vector3D ve2, Vector3D ve3) {
            double e1, e2, e3, det;
            det = ve3.x * ve2.y * ve1.z - ve2.x * ve3.y * ve1.z - ve3.x * ve1.y * ve2.z + ve1.x * ve3.y * ve2.z + ve2.x * ve1.y * ve3.z - ve1.x * ve2.y * ve3.z;
            e1 = -(-ve3.x * ve2.y * z + ve2.x * ve3.y * z + ve3.x * y * ve2.z - x * ve3.y * ve2.z - ve2.x * y * ve3.z + x * ve2.y * ve3.z);
            e2 = -(ve3.x * ve1.y * z - ve1.x * ve3.y * z - ve3.x * y * ve1.z + x * ve3.y * ve1.z + ve1.x * y * ve3.z - x * ve1.y * ve3.z);
            e3 = ve2.x * ve1.y * z - ve1.x * ve2.y * z - ve2.x * y * ve1.z + x * ve2.y * ve1.z + ve1.x * y * ve2.z - x * ve1.y * ve2.z;
            return new Vector3D(-e1, -e2, -e3);
        }
        //normalize the vector
        public void Normalize() {
            double norm = Math.Sqrt(x * x + y * y + z * z);
            x /= norm;
            y /= norm;
            z /= norm;
        }
        //get the string form
        public override string ToString() {
            return string.Format("({0:N3}, {1:N3}, {2:N3})", x, y, z);
        }
        //vector addition
        public static Vector3D operator +(Vector3D v1, Vector3D v2) {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        //vector subtraction
        public static Vector3D operator -(Vector3D v1, Vector3D v2) {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        //vector scaling
        public static Vector3D operator * (Vector3D vec,double a) {
            return new Vector3D(a * vec.X, a * vec.Y, a * vec.Z);
        }
    }

    //lowpass filter
    public class LowpassFilter {
        public double alpha;    //related to cutoff frequency, higher value means lower cutoff frequency
        public double output = 0.0; //output of filter
        //constructor
        public LowpassFilter(double alp) {
            alpha = alp;
        }
        //feed the filter with data
        public double Feed(double input) {
            return output = alpha * output + (1.0 - alpha) * input;
        }
    }

    //lowpass filter with peak cutoff (if (incoming input - previous output) > peak cutoff, the filter will use a far lower cutoff frequency)
    public class LowpassFilterCutoff {
        public double alpha, cutoff;
        public double output = 0.0;
        //constructor
        public LowpassFilterCutoff(double alp, double cut) {
            alpha = alp;
            cutoff = cut;
        }
        //feed the filter with data
        public double Feed(double input) {
            if (Math.Abs(input - output) < cutoff)
                output = alpha * output + (1.0 - alpha) * input;
            else
                output = Math.Pow(alpha, 0.2) * output + (1.0 - Math.Pow(alpha, 0.2)) * input;
            return output;
        }
    }
}
