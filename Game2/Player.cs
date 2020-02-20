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
    class Player : GameObject
    {




        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Bee");
        }
        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.S))
            {
            Drone drone = new Drone(new Vector2(0,0));
            GameWorld.Instance.gameObjectsToAdd.Add(new Drone);
            }
        }
    }
}
