using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace MuscleControllerFrontend {
    public enum Serialstate {
        Closed, //serial is not connected
        Single, //single shot mode
        Continuous  //continuous mode
    };

    //class for a single data record
    public class DataRecord {
        private byte timestamp;  //timestamp
        private short[] data = new short[10];   //data stored (order: AcX AcY AcZ GyX GyY GyZ Ch1 Ch2 Ch3 Ch4)
        private bool success;   //whether the data is successfully transfers

        public byte Timestamp { get => timestamp; set => timestamp = value; }
        public short[] Data { get => data; set => data = value; }
        public bool Success { get => success; set => success = value; }

        //constructor
        public DataRecord(byte tsp, bool succ) {
            Timestamp = tsp;
            Success = succ;
        }
    }

    //class for data buffer
    public class DataBuffer {
        /*
         * There are two data buffers storing incoming data records from the serial worker.
         * The serial worker receive one data record from Arduino, then make 2 copies of it and put them to the 2 buffers.
         * The reason of using 2 buffers to store the same data is that they are being 'consumed' (when data records are used, they are removed from the buffer)
         * by 2 different tasks (drawing charts and moving cursor) with different polling frequency, so using 1 buffer can be problematic.
         * There may be better solutions.
         */
        public List<DataRecord> chbuf = new List<DataRecord>(); //data buffer used by chart plotting timer
        public List<DataRecord> buf = new List<DataRecord>();   //data buffer used by cursor moving timer
        //constructor
        public DataBuffer() {
            chbuf.Clear();
            buf.Clear();
        }
    }

    //serial worker class
    public class SerialWorker {
        //main working loop for the thread
        public void DoWork() {
            //construct serial port object
            Serial1 = new SerialPort("COM4", 115200, Parity.None, 8, StopBits.One) {
                DtrEnable = false,
                RtsEnable = false,
                ReadTimeout = 500
            };

            while (running) {
                //check if serial port is open and there are remaining trigger counts (in single-shot mode where trigcount>=0)
                //or we are in continuous mode (trigcount=-1)
                if (Serial1.IsOpen && trigcount != 0) {
                    if (trigcount > 0) trigcount--; //if we are in single-shot mode, reduce trigger count by 1
                    byte[] bytsnd = new byte[1] { Globals.timestamp };  //data (1 byte, timestamp) to be sent to Arduino
                    byte[] bytrecv = new byte[21];  //data (1B timestamp + 10*2B data) to be received from Arduino
                    byte[] bytdata = new byte[20];  //data received from Arduino with the first timestamp byte removed
                    short[] data = new short[10];   //data received from Arduino converted from byte to short
                    int readnum = 0, offset = 0;    //bytes read from one read cycle and total bytes read
                    bool failed = false;    //if the reading process failed
                    Serial1.DiscardInBuffer();  //discard all remaining data in the serial port buffer to ensure data integrity
                    Serial1.Write(bytsnd, 0, 1);    //send the timestamp data to Arduino, then Arduino should respond with 21 bytes of data
                    do {    //it cannot be guaranteed that we would get all the data with one Serial.Read() because the serial port is not that fast and there may be remaining bytes to be transferred
                        try {
                            readnum = Serial1.Read(bytrecv, offset, 21 - offset);
                            offset += readnum;
                        } catch (TimeoutException) {
                            failed = true;  //when serial read timed out
                        }
                    } while (offset < 21 && failed == false);
                    Array.Copy(bytrecv, 1, bytdata, 0, 20); //remove the first timestamp byte
                    Buffer.BlockCopy(bytdata, 0, data, 0, 20);  //convert byte to short
                    //make data record object
                    DataRecord rec;
                    if (!failed) {
                        rec = new DataRecord(Globals.timestamp, true);
                        for (int i = 0; i < 10; i++) {
                            rec.Data[i] = data[i];
                        }
                    } else {
                        rec = new DataRecord(Globals.timestamp, false);
                        for (int i = 0; i < 10; i++) {
                            rec.Data[i] = 0;
                        }
                    }
                    lock (Globals.dbuf.buf) //lock the data buffers from being read and written by other processes when writing to them to prevent bad things
                        Globals.dbuf.buf.Add(rec);
                    lock (Globals.dbuf.chbuf)
                        Globals.dbuf.chbuf.Add(rec);
                    Globals.timestamp++;
                }
                //delay some time
                Thread.Sleep(15);
            }
        }
        //to open or close the serial port
        public void SetPortOpened(bool opened, string name) {
            if (opened) {
                if (!Serial1.IsOpen) {
                    Serial1.PortName = name;
                    Serial1.Open();
                }
            } else {
                if (Serial1.IsOpen) {
                    Serial1.Close();
                }
            }
        }
        //to set the remaining trigger count
        public void SetCount(int count) => trigcount = count;
        //to kill the process
        public void Kill() => running = false;

        private volatile int trigcount;
        private volatile bool running = true;
        public volatile SerialPort Serial1;
    }

}
