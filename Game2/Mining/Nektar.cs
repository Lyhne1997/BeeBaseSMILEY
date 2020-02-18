using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Mining
{
    public class Nektar : GameObject
    {

        static Semaphore MySemaphore = new Semaphore(0, 3);     // Max 3

        public string nektarInfo = "";
        public string nektarInfo2 = "";
        public string nektarInfo3 = "";
        public string nektarInfo4 = "";



        //public string release = "Realse Bees(3).";
        //public string release = "Realse Bees(3).";
        //public static string = (string)id + " Starts and waits outside to enter";

        public void MiningStart()
        {
            for (int i = 1; i <= 5; i++)
            {
                new Thread(Enter).Start(1);
            }
            Thread.Sleep(500);


            nektarInfo = "Release Bees(3).";

            //Debug.WriteLine("Realse Bees(3).");
            MySemaphore.Release(3); // Nektar is available
        }

        List<object> numbers = new List<object>();

        public void Enter(object id)
        {
            nektarInfo2 = id.ToString() + " Starts and waits outside to enter";

            MySemaphore.WaitOne();  // Only three bees in here!
            nektarInfo3 = id.ToString() + " Enters the Nektar Mine";

            Thread.Sleep(1000 * (int)id);
            nektarInfo4 = id.ToString() + " is leaving";

            MySemaphore.Release();


            Debug.WriteLine(id + " Starts and waits outside to enter");
            MySemaphore.WaitOne();  // Only three bees in here!
            Debug.WriteLine(id.ToString() + " Enters the Nektar Mine");
            Thread.Sleep(1000 * (int)id);
            Debug.WriteLine(id.ToString() + " is leaving");
            MySemaphore.Release();
        }


        public override void Update(GameTime gameTime)
        {

        }



        public override void LoadContent(ContentManager content)
        {
        }
    }
}