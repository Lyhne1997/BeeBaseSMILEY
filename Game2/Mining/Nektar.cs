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

        public bool nektarmine = true;

        //Nektar nek = new Nektar()
        //{
        //    hej = true;
        //}
        //public List<GameObject> gameObjects = new List<GameObject>();


        // if leavingBees then drone should go to base 
        // if waitingBees show sprites in front of the flower (not collecting)
        // if enteringBees hide sprite since they are "in" the flower

        // if enteringBees && postion = flower, then nektar++

        Drone drone = new Drone();


        public string nektarInfo = "";
        public string waitingBees = "";
        public string enteringBees = "";
        public string leavingBees = "";

        public string minedNektar = "";
        public string remainingNektar = "";

        public bool beesIsEntered = true;


        static readonly object lockObject = new object();
        static Mutex m = new Mutex();
        private int state;

        //private static Random random;
        public void StartShit()
        {
            for (int i = 0; i < 2; i++)
            {
                Thread t = new Thread(RunMe);
                t.IsBackground = true;
                t.Start();
            }
        }
        /*Husk at count tælles ned hver gang en tråd får adgang. Så når den er 0
        er der tre tråde inde og nye tråde må vente. Når en tråde forlader
        tælles count op. Release(3) kan sørge for at alle kommer ud, dvs.
        tømme natklubben. Release(), tæller blot en op sv.t. en enkelt tråd der
        forlader */

        public void RunMe()
        {
            int remainingNektarInMine = 100;
            int nektar = 0;
            /// nektarmine == true Should be changed since it stops the thread after the mine
            /// has run out of resources
            while (nektarmine == true)
            {


                if (m.WaitOne(500))
                {
                    if (state == 5)
                    {
                        state++;
                        Trace.Assert(state == 6, "Race Condition in Loop" + nektar);
                    }
                    state = 5;
                    nektar++;
                }
                else
                {


                    //if bees enter == true then > mine

                    //if (enteringBees == "Bob-bi(1) kommer ind")
                    if (isCollectingFlowerA == true)
                    {
                        remainingNektar = $"You mined 1 Nektar, remaining Nektar:{remainingNektarInMine}";

                        // Debug
                        Debug.WriteLine($"You mined 1 Nektar, remaining Nektar:{remainingNektarInMine}");

                        remainingNektarInMine--;
                        nektar++;
                        minedNektar = $"You have mined a total of {nektar} Nektar";

                        // Debug
                        Debug.WriteLine($"You have mined a total of {nektar} Nektar");
                    }

                    //}



                    if (remainingNektarInMine <= 0)
                    {
                        // Kill flower if resources/nektar is 0
                        GameWorld.Instance.flowerIsAlive = false;

                        Debug.WriteLine("The mine is out of Nektar!");
                    }
                    if (remainingNektarInMine == 0)
                    {

                        Debug.WriteLine("Press any key to refill the mine");
                        Thread.Sleep(1000);
                        remainingNektarInMine = 100;
                        nektarmine = false;
                    }

                    Thread.Sleep(50);
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
            while (nektarmine == true)
            {
                if ((int)id == 1)
                {

                    waitingBees = "Bob-bi 1 venter uden for";

                    //drone.flowerAInput = true;
                    //drone.flowerBInput = false;
                    //drone.flowerCInput = false;

                    movesTowardFlower = true;

                    //isMovingToFlowerA = false;
                }

                if ((int)id == 2)
                {
                    waitingBees = "Claus 2 venter uden for";
                }
                if ((int)id == 3)
                {
                    waitingBees = "TonnyBonde 3 venter uden for";
                }
                if ((int)id == 4)
                {
                    waitingBees = "Karsten 4 venter uden for";
                }
                if ((int)id == 5)
                {
                    waitingBees = "Lonni 5 venter uden for";
                }
                MySemaphore.WaitOne();  // Only three bees in here!


                if ((int)id == 1)
                {
                    enteringBees = "Bob-bi(1) kommer ind";
                    movesTowardFlower = false;


                }
                else if ((int)id == 2)
                {
                    enteringBees = "Claus(2) kommer ind";
                }
                else if ((int)id == 3)
                {
                    enteringBees = "TonnyBonde(3) kommer ind";
                }
                else if ((int)id == 4)
                {
                    enteringBees = "Karsten(4) kommer ind";
                }
                else if ((int)id == 5)
                {
                    enteringBees = "Lonni(5) kommer ind";
                }

                Thread.Sleep(1000 * (int)id);
                //Debug.WriteLine(id.ToString() + " is leaving");

                if ((int)id == 1)
                {
                    leavingBees = "Bob-bi(1) forlader";
                }
                else if ((int)id == 2)
                {
                    leavingBees = "Claus(2) forlader";
                }
                else if ((int)id == 3)
                {
                    leavingBees = "TonnyBonde(3) forlader";
                }
                else if ((int)id == 4)
                {
                    leavingBees = "Karsten(4) forlader";
                }
                else if ((int)id == 5)
                {
                    leavingBees = "Lonni(5) forlader";
                }

                MySemaphore.Release();
            }




            #region Debug
            //if ((int)id == 1)
            //{
            //    Debug.WriteLine("Bob-bi(1) venter uden for");
            //}
            //if ((int)id == 2)
            //{
            //    Debug.WriteLine("Claus(2) venter uden for");
            //}
            //if ((int)id == 3)
            //{
            //    Debug.WriteLine("TonnyBonde(3) venter uden for");
            //}
            //if ((int)id == 4)
            //{
            //    Debug.WriteLine("Karsten(4) venter uden for");
            //}
            //if ((int)id == 5)
            //{
            //    Debug.WriteLine("Søren(5) venter uden for");
            //}
            //MySemaphore.WaitOne();  // Only three bees in here!
            //                        //Debug.WriteLine(id.ToString() + " Enters the Nektar Mine");


            //if ((int)id == 1)
            //{
            //    Debug.WriteLine("Bob-bi(1) kommer ind");
            //}
            //if ((int)id == 2)
            //{
            //    Debug.WriteLine("Claus(2) kommer ind");
            //}
            //if ((int)id == 3)
            //{
            //    Debug.WriteLine("TonnyBonde(3) kommer ind");
            //}
            //if ((int)id == 4)
            //{
            //    Debug.WriteLine("Karsten(4) kommer ind");
            //}
            //if ((int)id == 5)
            //{
            //    Debug.WriteLine("Søren(5) kommer ind");
            //}

            //Thread.Sleep(1000 * (int)id);
            ////Debug.WriteLine(id.ToString() + " is leaving");

            //if ((int)id == 1)
            //{
            //    Debug.WriteLine("Bob-bi(1) forlader");
            //}
            //if ((int)id == 2)
            //{
            //    Debug.WriteLine("Claus(2) forlader");
            //}
            //if ((int)id == 3)
            //{
            //    Debug.WriteLine("TonnyBonde(3) forlader");
            //}
            //if ((int)id == 4)
            //{
            //    Debug.WriteLine("Karsten(4) forlader");
            //}
            //if ((int)id == 5)
            //{
            //    Debug.WriteLine("Søren(5) forlader");
            //}

            //MySemaphore.Release();

            #endregion Debug

        }


        public override void Update(GameTime gameTime)
        {
        }



        public override void LoadContent(ContentManager content)
        {
        }
    }
}
