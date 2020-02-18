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

        static readonly object lockObject = new object();
        static Mutex m = new Mutex();

        //private static Random random;
        public void StartShit()
        {
            for (int i = 0; i < 2; i++)
            {
                Thread t = new Thread(RunMe);
                //t.IsBackground = true;
                t.Start();
            }
        }

        public void RunMe()
        {
            bool goldInMine1 = true;
            int i = 100;
            int gold = 0;
            while (goldInMine1 == true)
            {


                Debug.WriteLine($"You mined 1 Gold, remaining Gold:{i}");
                i--;
                gold++;
                if (i <= 0)
                {
                    Debug.WriteLine("The mine is out of gold!");
                    Debug.WriteLine($"You have mined a total of {gold} Gold");
                }
                if (i == 0)
                {
                    goldInMine1 = false;
                    Debug.WriteLine("Press any key to refill the mine");
                    Thread.Sleep(10000);
                    i = 100;
                    goldInMine1 = true;
                }


            }
        }



        public void MiningStart()
        {
         

            for (int i = 1; i <= 5; i++)
            {
                new Thread(Enter).Start(i);
            }
            Thread.Sleep(500);


            nektarInfo = "Release Bees(3).";

            //Debug.WriteLine("Realse Bees(3).");
            MySemaphore.Release(3); // Nektar is available
        }


        public void Enter(object id)
        {
            //nektarInfo2 = id.ToString() + " Starts and waits outside to enter";

            //MySemaphore.WaitOne();  // Only three bees in here!
            //nektarInfo3 = id.ToString() + " Enters the Nektar Mine";

            //Thread.Sleep(1000 * (int)id);
            //nektarInfo4 = id.ToString() + " is leaving";

            //MySemaphore.Release();




            //Debug.WriteLine(id + " Starts and waits outside to enter");
            if ((int)id == 1)
            {
                Debug.WriteLine("1 venter uden for");
            }
            if ((int)id == 2)
            {
                Debug.WriteLine("2 venter uden for");
            }
            if ((int)id == 3)
            {
                Debug.WriteLine("3 venter uden for");
            }
            if ((int)id == 4)
            {
                Debug.WriteLine("4 venter uden for");
            }
            if ((int)id == 5)
            {
                Debug.WriteLine("5 venter uden for");
            }
            MySemaphore.WaitOne();  // Only three bees in here!
                                    //Debug.WriteLine(id.ToString() + " Enters the Nektar Mine");


            if ((int)id == 1)
            {
                Debug.WriteLine("1 kommer ind");
            }
            if ((int)id == 2)
            {
                Debug.WriteLine("2 kommer ind");
            }
            if ((int)id == 3)
            {
                Debug.WriteLine("3 kommer ind");
            }
            if ((int)id == 4)
            {
                Debug.WriteLine("4 kommer ind");
            }
            if ((int)id == 5)
            {
                Debug.WriteLine("5 kommer ind");
            }

            Thread.Sleep(1000 * (int)id);
            //Debug.WriteLine(id.ToString() + " is leaving");

            if ((int)id == 1)
            {
                Debug.WriteLine("1 forlader");
            }
            if ((int)id == 2)
            {
                Debug.WriteLine("2 forlader");
            }
            if ((int)id == 3)
            {
                Debug.WriteLine("3 forlader");
            }
            if ((int)id == 4)
            {
                Debug.WriteLine("4 forlader");
            }
            if ((int)id == 5)
            {
                Debug.WriteLine("5 forlader");
            }

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