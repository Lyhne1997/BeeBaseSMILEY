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


namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 

    public class GameWorld : Game
    {

        //GameConsole console = new GameConsole(this, spriteBatch); // where `this` is your `Game` class


        Nektar nek = new Nektar();

        public List<GameObject> gameObjects = new List<GameObject>();

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public SpriteEffects spriteEffects;
        Vector2 position;
        Texture2D texture;

        public SpriteFont text1;
        public static GameWorld Instance;

        private static int stateS;
        static readonly object lockObject = new object();
        static Mutex m = new Mutex();
        private static Random random;


        //static void RunMe()
        //{
        //    bool goldInMine1 = true;
        //    for (int f = 0; f < 2; f++)
        //    {
        //        Thread t = new Thread(RunMe);
        //        t.Start();
        //    }
        //    int i = 100;
        //    int gold = 0;

        //    while (goldInMine1 == true)
        //    {
        //        if (m.WaitOne(50))
        //        {
        //            if (stateS == 5)
        //            {
        //                stateS++;
        //                Trace.Assert(stateS == 6, "Race Condition in Loop" + i);
        //            }
        //            stateS = 5;
        //            i++;
        //        }
        //        else
        //        {

        //            Debug.WriteLine($"You mined 1 Gold, remaining Gold:{i}");
        //            i--;
        //            gold++;
        //            if (i <= 0)
        //            {
        //                Debug.WriteLine("The mine is out of gold!");
        //                Debug.WriteLine($"You have mined a total of {gold} Gold");
        //            }
        //            if (i == 0)
        //            {
        //                goldInMine1 = false;
        //                Debug.WriteLine("Press any key to refill the mine");
        //                //Debug.ReadKey();
        //                i = 100;
        //                goldInMine1 = true;
        //            }
        //        }

        //    }

        //}





        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            nek.RunMe();
            nek.MiningStart();
            nek.StartShit();
            base.Initialize();
            position = new Vector2(graphics.GraphicsDevice.Viewport.
                 Width / 2,
                              graphics.GraphicsDevice.Viewport.
                              Height / 2);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = this.Content.Load<Texture2D>("Bee");

            text1 = Content.Load<SpriteFont>("text");

            // TODO: use this.Content to load your game content here
            gameObjects.Add(new Player());
            //gameObjects.Add(new Mine(new Vector2(200, 100));

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState state = Mouse.GetState();

            // Update our sprites position to the current cursor location
            position.X = state.X;
            position.Y = state.Y;

            // Check if Right Mouse Button pressed, if so, exit
            if (state.RightButton == ButtonState.Pressed)
                Exit();
            // TODO: Add your update logic here


            //Debug.WriteLine(position.X.ToString() +
            //                    "," + position.Y.ToString());
            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, origin: new Vector2(64, 64));

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }

            spriteBatch.DrawString(text1, nek.nektarInfo,
                                          new Vector2(100, graphics.GraphicsDevice.Viewport.Height / 2),
                                          Color.White,
                                          0,
                                          Vector2.Zero,
                                          1,
                                          SpriteEffects.None,
                                          1f);


            spriteBatch.DrawString(text1, nek.nektarInfo2,
                                        new Vector2(100, graphics.GraphicsDevice.Viewport.Height / 2 + 130),
                                        Color.White,
                                        0,
                                        Vector2.Zero,
                                        1,
                                        SpriteEffects.None,
                                        1f);

            spriteBatch.DrawString(text1, nek.nektarInfo3,
                                        new Vector2(100, graphics.GraphicsDevice.Viewport.Height / 2 + 160),
                                        Color.White,
                                        0,
                                        Vector2.Zero,
                                        1,
                                        SpriteEffects.None,
                                        1f);

            spriteBatch.DrawString(text1, nek.nektarInfo4,
                                        new Vector2(100, graphics.GraphicsDevice.Viewport.Height / 2 + 190),
                                        Color.White,
                                        0,
                                        Vector2.Zero,
                                        1,
                                        SpriteEffects.None,
                                        1f);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
