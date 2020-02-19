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
        private Vector2 baseA;
        private Vector2 flowerA;
        private Vector2 flowerB;
        private Vector2 flowerC;
        //Når bien først spawner og venter på input fra spilleren.
        private bool isWaitingForInput = true;
        //Når bien har samlet Nectar og skal bevæge sig mod basen, ændrer sig afhængigt af hvilken blomst den var ved.
        /*private bool isMovingToBaseAFromFlowerA = false;
        private bool isMovingToBaseAFromFlowerB = false;
        private bool isMovingToBaseAFromFlowerC = false;*/
        private bool isMovingToBaseA = false;
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

        private Vector2 direction;
        protected float rotation;
        private Vector2 distance;
        public float speed;

        public Drone(Vector2 position)
        {
            this.position = position;
            flowerA.X = 650;
            flowerA.Y = 300;
            baseA.X = 0;
            baseA.Y = 0;

            speed = 5f;
        }
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Bee");
        }
        public override void Update(GameTime gameTime)
        {
            DroneManagement(gameTime);
        }
        private void DroneManagement(GameTime gameTime)
        {
            if (isMovingToFlowerA == true)
            {
                distance.X = flowerA.X - this.position.X;
                distance.Y = flowerA.Y - this.position.Y;
            }
            if (isMovingToBaseA == true)
            {
                distance.X = baseA.X - this.position.X;
                distance.Y = baseA.Y - this.position.Y;
            }

            rotation = (float)Math.Atan2(distance.X, -distance.Y);
            direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - rotation));
            float positiveDistanceX = distance.X;
            float positiveDistanceY = distance.Y;

            if (distance.X < 0)
            {
                positiveDistanceX *= -1;
                positiveDistanceY *= -1;
            }

            if (distance.Y < 0)
            {
                positiveDistanceX *= -1;
                positiveDistanceY *= -1;
            }
            if (this.position.X <= baseA.X && this.position.Y <= baseA.Y)
            {
                isMovingToBaseA = false;
                isMovingToFlowerA = true;
            }
            if (this.position.X >= flowerA.X && this.position.Y >= flowerA.Y)
            {
                isMovingToFlowerA = false;
                isMovingToBaseA = true;
            }


            position += direction * this.speed;
        }
    }
}
