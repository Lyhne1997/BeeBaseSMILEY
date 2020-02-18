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




        /* bool kør = true;
          while(kør == true)
          {
          MyTask myTask = new MyTask("Hello World");
          myTask.Start();
          for(int i = 0; i < 100; i++)
          {
              MyTask myTask2 = new MyTask("Hej");
              myTask2.isBackground = true;
              myTask2.Start();
          }
              kør = false;
          }*/


        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Bee");
        }
        public override void Update(GameTime gameTime)
        {

        }
    }
}
