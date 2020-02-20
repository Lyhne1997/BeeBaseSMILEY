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
using Game2.Mining;
using Microsoft.Xna.Framework.Media;

namespace Game2
{
    class Base : GameObject
    {

        public Base()
        {
            position = new Vector2(0, 0);
        }
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[3];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>(i + 1 + ("hive_bees"));
            }

            fps = 3;

            sprite = sprites[0];

        }


        public override void Update(GameTime gameTime)
        {
            Animation(gameTime);
        }
    }
}
