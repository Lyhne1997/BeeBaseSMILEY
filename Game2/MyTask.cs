//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Game2
//{
//    public class MyTask
//    {
//        Thread internalThread;
//        string data;
//        internal bool isBackground;

//        public MyTask(string data)
//        {
//            this.data = data;
//            internalThread = new Thread(ThreadMethod);
//        }

//        private void ThreadMethod()
//        {
//            for (int f = 0; f < 2; f++)
//            {
//                Thread t = new Thread(RunMe);
//                t.Start();
//            }
//            Debug.WriteLine("The thread recieved the following message: : {0}", data);
//        }

//        public void Start()
//        {
//            internalThread.Start();
//        }
//    }
//}
