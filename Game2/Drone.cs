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
        //Når bien først spawner og venter på input fra spilleren.
        private bool isWaitingForInput = true;
        //Når bien har samlet Nectar og skal bevæge sig mod basen, ændrer sig afhængigt af hvilken blomst den var ved.
        private bool isMovingToBaseAFromFlowerA = false;
        private bool isMovingToBaseAFromFlowerB = false;
        private bool isMovingToBaseAFromFlowerC = false;
        //Når bien har fået input fra spilleren og får besked på at bevæge sig til en blomst for at hente Nectar.
        private bool isMovingToFlowerA = false;
        private bool isMovingToFlowerB = false;
        private bool isMovingToFlowerC = false;
        //Når bien er ved blomsten og samler Nectar.
        private bool isCollectingFlowerA = false;
        private bool isCollectingFlowerB = false;
        private bool isCollectingFlowerC = false;
        //Når bien er kommet til basen med Nectar og skal aflevere Nectar.
        private bool isOffloadingNectar = false;

        //Retningen som bien skal bevæge sig immod.
        private Vector2 direction;
        //Roterer bien afhængigt af hvilken retning den har.
        protected float rotation;
        //Afstanden fra bien og det mål som den skal hen til.
        private Vector2 distance;

        public Drone()
        {
            //Position på Bien.

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
            //Biernes hastighed.
            speed = 10f;
        }
        public override void LoadContent(ContentManager content)
        {
            //Loader vores sprite.
            //sprite = content.Load<Texture2D>("bee1");

            sprites = new Texture2D[3];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>(i + 1 + ("bee"));
            }

            fps = 3;

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
   


            //Udregner afstanden fra bien til Flower "A" eller Basen afhængigt af hvilken den skal bevæge sig imod.
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {

                if (isMovingToFlowerA == true)
                {
                    distance.X = flowerA.X - this.position.X;
                    distance.Y = flowerA.Y - this.position.Y;
                }
                if (isMovingToBaseAFromFlowerA == true)
                {
                    distance.X = baseA.X - this.position.X;
                    distance.Y = baseA.Y - this.position.Y;
                }
                isWaitingForInput = false;
            }
            ////Udregner afstanden fra bien til Flower "B" eller Basen afhængigt af hvilken den skal bevæge sig imod.
            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {

                if (isMovingToFlowerB == true)
                {
                    distance.X = flowerB.X - this.position.X;
                    distance.Y = flowerB.Y - this.position.Y;
                }
                if (isMovingToBaseAFromFlowerB == true)
                {
                    distance.X = baseA.X - this.position.X;
                    distance.Y = baseA.Y - this.position.Y;
                }
                isWaitingForInput = false;
            }
            ////Udregner afstanden fra bien til Flower "C" eller Basen afhængigt af hvilken den skal bevæge sig imod.
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {

                if (isMovingToFlowerC == true)
                {
                    distance.X = flowerC.X - this.position.X;
                    distance.Y = flowerC.Y - this.position.Y;
                }
                if (isMovingToBaseAFromFlowerC == true)
                {
                    distance.X = baseA.X - this.position.X;
                    distance.Y = baseA.Y - this.position.Y;
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
                isMovingToFlowerA = true;
            }
            if (this.position.X >= flowerA.X && this.position.Y >= flowerA.Y)
            {
                isMovingToFlowerA = false;
                isMovingToBaseAFromFlowerA = true;
            }
            //Bestemmer hvilken position bien skal bevæge sig imod ved hjælp af booleans.
            //Dette er mellem Basen og Flower "B".
            if (this.position.X <= baseA.X || this.position.Y <= baseA.Y)
            {
                isMovingToBaseAFromFlowerB = false;
                isMovingToFlowerB = true;
            }
            if (this.position.X >= flowerB.X || this.position.Y >= flowerB.Y)
            {
                isMovingToFlowerB = false;
                isMovingToBaseAFromFlowerB = true;
            }
            //Bestemmer hvilken position bien skal bevæge sig imod ved hjælp af booleans.
            //Dette er mellem Basen og Flower "C".
            if (this.position.X <= baseA.X || this.position.Y <= baseA.Y)
            {
                isMovingToBaseAFromFlowerC = false;
                isMovingToFlowerC = true;
            }
            if (this.position.X >= flowerC.X || this.position.Y >= flowerC.Y)
            {
                isMovingToFlowerC = false;
                isMovingToBaseAFromFlowerC = true;
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
