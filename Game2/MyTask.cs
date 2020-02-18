//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Game2.Mining;

//namespace Game2
//{
//    public class MyTask : GameObject
//    {
        
//        string data;
//        internal bool isBackground;
//        Nektar nek = new Nektar();
//        public MyTask(string data)
//        {
//            this.data = data;
//            internalThread = new Thread(ThreadMethod);
//        }

//        private void ThreadMethod()
//        {
//            for (int f = 0; f < 2; f++)
//            {
//                Thread t = new Thread(nek.RunMe);
//                t.Start();
//            }
//            Debug.WriteLine("The thread recieved the following message: : {0}", data);
//        }

//        public void Start()
//        {
//            isBackground = true;
//            internalThread.Start();
//        }

//        public override void LoadContent(ContentManager content)
//        {
        
//        }

//        public override void Update(GameTime gameTime)
//        {
         
//        }
//    }
//}
