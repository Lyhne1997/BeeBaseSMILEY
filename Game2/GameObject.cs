using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Game2
{
    public abstract class GameObject
    {
        //protected Vector2 baseA;
        //protected Vector2 flowerA;
        //protected Vector2 flowerB;
        //protected Vector2 flowerC;

        protected static bool movesTowardFlower = false;
        protected static bool isMovingToFlowerA = false;

        private int currentIndex;
        //
        private float timeElapsed;
        protected Texture2D[] sprites;
        protected Texture2D sprite;
        protected int fps;

    

        protected Vector2 direction;
        protected float rotation;
        protected Vector2 distance;


        public Thread internalThread;

        protected Vector2 position;

        protected float speed;

        protected Vector2 origin;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 1);

        }
        protected void Animation(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentIndex = (int)(timeElapsed * fps);
            sprite = sprites[currentIndex];

            if (currentIndex >= sprites.Length - 1)
            {
                timeElapsed = 0;
                currentIndex = 0;
            }
        }

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gameTime);
    }
}
