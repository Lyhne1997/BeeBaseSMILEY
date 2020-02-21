using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game2
{
    class Drone : GameObject
    {


        //Vectorer til de forskellige assets.
        private Vector2 baseA;
        private Vector2 flowerA;
        private Vector2 flowerB;
        private Vector2 flowerC;
        //
        private Vector2 waitingFlowerA;
        private Vector2 waitingFlowerB;
        private Vector2 waitingFlowerC;
        //Bool så man kun skal give input én gang for at få bien til at bevæge sig mod dets target.
        private bool flowerAInput = false;
        private bool flowerBInput = false;
        private bool flowerCInput = false;
        //Timer
        public int timer;
        private bool isWaitingForInput;
        private bool isMovingToFlowerB;
        private bool isMovingToBaseAFromFlowerB;
        private bool isMovingToFlowerC;
        private bool isMovingToBaseAFromFlowerC;
        //
        private bool isWaitingToCollectFlowerA;
        private bool isWaitingToCollectFlowerB;
        private bool isWaitingToCollectFlowerC;
        //private bool isCollectingFlowerA;
        private bool isCollectingFlowerB;
        private bool isCollectingFlowerC;

        public Drone(Vector2 position, bool flowerAInput, bool flowerBInput, bool flowerCInput)
        {
            this.flowerAInput = flowerAInput;
            this.flowerBInput = flowerBInput;
            this.flowerCInput = flowerCInput;
            //Position på Bien.
            //this.position = position;
            this.position = position;
            //this.isMovingToFlowerB = isMovingToFlowerB;
            //Position på Basen.    
            baseA.X = 40;
            baseA.Y = 40;
            //Position på Flower A.
            flowerA.X = 45;
            flowerA.Y = 530;
            //Position på Flower B.
            flowerB.X = 850;
            flowerB.Y = 650;
            //Position på Flower C.
            flowerC.X = 820;
            flowerC.Y = 55;
            //
            waitingFlowerA.X = 45;
            waitingFlowerA.Y = 480;
            //
            waitingFlowerB.X = 800;
            waitingFlowerB.Y = 600;
            //
            waitingFlowerC.X = 770;
            waitingFlowerC.Y = 55;
            //Biernes hastighed.
            speed = 10f;
        }

   

        //public static void Sleep(int millisecondsTimeout);

        public override void LoadContent(ContentManager content)
        {
            //Loader vores sprite.
            //sprite = content.Load<Texture2D>("bee1");
            sprites = new Texture2D[3];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>(i + 1 + ("bee"));
            }

            fps = 8;

            sprite = sprites[0];


        }
        public override void Update(GameTime gameTime)
        {
            Animation(gameTime);

            //Updaterer movement for hver "gametick".
            DroneManagement(gameTime);
        }
        private void DroneManagement(GameTime gameTime)
        {
            //Player input til biens movement.
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                flowerAInput = true;
                flowerBInput = false;
                flowerCInput = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {
                flowerAInput = false;
                flowerBInput = true;
                flowerCInput = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                flowerAInput = false;
                flowerBInput = false;
                flowerCInput = true;
            }

            //Udregner afstanden fra bien til Flower "A" eller Basen afhængigt af hvilken den skal bevæge sig imod.
            if (flowerAInput == true && flowerBInput == false && flowerCInput == false)
            {

                if (isWaitingToCollectFlowerA == true)
                {
                    {
                        distance.X = waitingFlowerA.X - this.position.X;
                        distance.Y = waitingFlowerA.Y - this.position.Y;
                    }
                }
                if (isCollectingFlowerA == true)
                {
                    {
                        distance.X = flowerA.X - this.position.X;
                        distance.Y = flowerA.Y - this.position.Y;
                    }
                }
                if (isMovingToBaseAFromFlowerA == true)
                {
                    {
                        distance.X = baseA.X - this.position.X;
                        distance.Y = baseA.Y - this.position.Y;
                    }
                }
                isWaitingForInput = false;
            }
            //Udregner afstanden fra bien til Flower "B" eller Basen afhængigt af hvilken den skal bevæge sig imod.
            if (flowerBInput == true && flowerAInput == false && flowerCInput == false)
            {

                if (isWaitingToCollectFlowerB == true)
                {
                    {
                        {
                        distance.X = waitingFlowerB.X - this.position.X;
                        distance.Y = waitingFlowerB.Y - this.position.Y;
                        }
                    }
                    if (isCollectingFlowerB == true)
                    {
                        {
                        distance.X = flowerB.X - this.position.X;
                        distance.Y = flowerB.Y - this.position.Y;
                        }
                    }
                    if (isMovingToBaseAFromFlowerB == true)
                    {
                        { 
                        distance.X = baseA.X - this.position.X;
                        distance.Y = baseA.Y - this.position.Y;
                        }
                    }
                }
                isWaitingForInput = false;
            }
            ////Udregner afstanden fra bien til Flower "C" eller Basen afhængigt af hvilken den skal bevæge sig imod.
            if (flowerCInput == true && flowerAInput == false && flowerBInput == false)
            {

                if (isWaitingToCollectFlowerC == true)
                {
                    {
                        distance.X = waitingFlowerC.X - this.position.X;
                        distance.Y = waitingFlowerC.Y - this.position.Y;
                    }
                }
                if (isCollectingFlowerC == true)
                {
                    {
                        distance.X = flowerC.X - this.position.X;
                        distance.Y = flowerC.Y - this.position.Y;
                    }
                }
                if (isMovingToBaseAFromFlowerC == true)
                {
                    {
                        distance.X = baseA.X - this.position.X;
                        distance.Y = baseA.Y - this.position.Y;
                    }
                }
                isWaitingForInput = false;
            }
            //Udregner rotation på bien afhængigt af afstanden mellem bien og dets "target".
            rotation = (float)Math.Atan2(distance.X, -distance.Y);
            //Udregner hvilket retning som bien skal bevæge sig imod.
            direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - rotation));

            //Bestemmer hvilken position bien skal bevæge sig imod ved hjælp af booleans.
            //Dette er mellem Basen og Flower "A".
            if (this.position.X <= baseA.X && this.position.Y <= baseA.Y)
            {
                isMovingToBaseAFromFlowerA = false;
                isMovingToFlowerA = false;
                isWaitingToCollectFlowerA = true;
                timer = 0;
            }
            if (this.position.X >= waitingFlowerA.X && this.position.Y >= waitingFlowerA.Y)
            {
                isWaitingToCollectFlowerA = true;
                if (timer >= 500)
                {
                    isWaitingToCollectFlowerA = false;
                    isCollectingFlowerA = true;
                }
            }

            if (this.position.X >= flowerA.X && this.position.Y >= flowerA.Y)
            {
                isCollectingFlowerA = true;
                if (timer >= 1000)
                {
                    isMovingToFlowerA = false;
                    isMovingToBaseAFromFlowerA = true;
                    isCollectingFlowerA = false;
                }
                timer++;
            }
            //Bestemmer hvilken position bien skal bevæge sig imod ved hjælp af booleans.
            //Dette er mellem Basen og Flower "B".132
            if (this.position.X <= baseA.X || this.position.Y <= baseA.Y)
            {
                isMovingToBaseAFromFlowerB = false;
                isMovingToFlowerB = false;
                isWaitingToCollectFlowerB = true;
                timer = 0;
            }
            if (this.position.X >= waitingFlowerB.X && this.position.Y >= waitingFlowerB.Y)
            {
                isWaitingToCollectFlowerB = true;
                if (timer >= 500)
                {
                    isWaitingToCollectFlowerB = false;
                    isCollectingFlowerB = true;
                }
            }
            if (this.position.X >= flowerB.X || this.position.Y >= flowerB.Y)
            {
                isCollectingFlowerB = true;
                if (timer >= 1000)
                {
                    isMovingToFlowerB = false;
                    isMovingToBaseAFromFlowerB = true;
                    isCollectingFlowerB = false;
                }
                timer++;
            }
            //Bestemmer hvilken position bien skal bevæge sig imod ved hjælp af booleans.
            //Dette er mellem Basen og Flower "C".
            if (this.position.X <= baseA.X || this.position.Y <= baseA.Y)
            {
                isMovingToBaseAFromFlowerC = false;
                isMovingToFlowerC = false;
                isWaitingToCollectFlowerC = true;
                timer = 0;
            }
            if (this.position.X >= waitingFlowerC.X && this.position.Y >= waitingFlowerC.Y)
            {
                isWaitingToCollectFlowerC = true;
                if (timer >= 500)
                {
                    isWaitingToCollectFlowerC = false;
                    isCollectingFlowerC = true;
                }
            }
            if (this.position.X >= flowerC.X || this.position.Y >= flowerC.Y)
            {
                isCollectingFlowerC = true;
                if (timer >= 1000)
                {
                    isMovingToFlowerC = false;
                    isMovingToBaseAFromFlowerC = true;
                    isCollectingFlowerC = false;
                }
                timer++;
            }
            //Bool så bien står stiller mens den venter på spillerens input.
            if (isWaitingForInput == false)
            {
                //Bevæger bien baseret på direction og speed.
                position += direction * this.speed;
            }
            //Så biens står stille når den ikke får input.
            isWaitingForInput = true;
        }
    }
}
