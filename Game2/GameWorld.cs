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
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 

    public class GameWorld : Game
    {
        public List<GameObject> gameObjects = new List<GameObject>();
        Nektar nek = new Nektar();
        Drone drone = new Drone();

        // Background sprite
        private Texture2D background;

        // Flower sprites
        private Texture2D flowerA_sprite;
        private Texture2D flowerB_sprite;
        private Texture2D flowerC_sprite;
        private Texture2D flowerA_sprite_dead;

        public bool flowerIsAlive = false;

        public List<GameObject> gameObjects = new List<GameObject>();
        public List<GameObject> gameObjectsToAdd = new List<GameObject>();
        public List<GameObject> gameObjectsToRemove = new List<GameObject>();

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public SpriteEffects spriteEffects;
        Vector2 position;
        Texture2D texture;

        public SpriteFont text1;
        private Song backgroundSound;
        public static GameWorld Instance;

        private static int stateS;
        static readonly object lockObject = new object();
        static Mutex m = new Mutex();
        private static Random random;

        private static Vector2 screenSize;

        public static Vector2 ScreenSize
        {
            get { return screenSize; }
        }

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            screenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Instance = this;
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
            //nek.RunMe();
            nek.StartShit();
            nek.MiningStart();
            base.Initialize();

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

            background = Content.Load<Texture2D>("Background");

            // flower alive sprites
            flowerA_sprite = Content.Load<Texture2D>("flower_dead");
            flowerB_sprite = Content.Load<Texture2D>("flower_dead");
            flowerC_sprite = Content.Load<Texture2D>("flower_dead");

            // flower dead sprites
            flowerA_sprite = Content.Load<Texture2D>("flower_alive");
            flowerB_sprite = Content.Load<Texture2D>("flower_alive");
            flowerC_sprite = Content.Load<Texture2D>("flower_alive");

            // Sound
            backgroundSound = Content.Load<Song>("Happy_Dreams.Background");
            MediaPlayer.Play(backgroundSound);
            MediaPlayer.Volume -= 0.5f;
            MediaPlayer.IsRepeating = true;

            // TODO: use this.Content to load your game content here
            gameObjects.Add(new Base());

            gameObjects.Add(new Drone());

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Kill flower
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                flowerIsAlive = true;
                //flowerA_sprite = Content.Load<Texture2D>("flower_dead");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                flowerIsAlive = false;

                //flowerA_sprite = Content.Load<Texture2D>("flower_alive");
            }



            if (flowerIsAlive == true)
            {
                flowerA_sprite = Content.Load<Texture2D>("flower_dead");
            }
            if (flowerIsAlive == false)
            {
                flowerA_sprite = Content.Load<Texture2D>("flower_alive");
            }


            //MouseState state = Mouse.GetState();

            //// Update our sprites position to the current cursor location
            //position.X = state.X;
            //position.Y = state.Y;

            //// Check if Right Mouse Button pressed, if so, exit
            //if (state.RightButton == ButtonState.Pressed)
            //    Exit();


            //// Test to see if mouse reacts when hitting a certain point on the screen
            //if (state.X >= 100 && state.X <= 120)
            //{
            //    Debug.WriteLine("hit");
            //}


            //// TODO: Add your update logic here

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }

            gameObjects.AddRange(gameObjectsToAdd);
            gameObjects.RemoveAll(go => gameObjectsToRemove.Contains(go));
            gameObjectsToRemove.Clear();
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

            spriteBatch.Draw(background, position, origin: new Vector2(0, 0));


            spriteBatch.Draw(flowerA_sprite, new Vector2(850, 600));
            spriteBatch.Draw(flowerB_sprite, new Vector2(850, 0));
            spriteBatch.Draw(flowerC_sprite, new Vector2(0, 600));






            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }

            spriteBatch.DrawString(text1, nek.nektarInfo,
                                          new Vector2(50, graphics.GraphicsDevice.Viewport.Height / 2),
                                          Color.White,
                                          0,
                                          Vector2.Zero,
                                          1,
                                          SpriteEffects.None,
                                          1f);


            spriteBatch.DrawString(text1, nek.waitingBees,
                                        new Vector2(100, graphics.GraphicsDevice.Viewport.Height / 2 + 80),
                                        Color.White,
                                        0,
                                        Vector2.Zero,
                                        1,
                                        SpriteEffects.None,
                                        1f);

            spriteBatch.DrawString(text1, nek.enteringBees,
                                        new Vector2(100, graphics.GraphicsDevice.Viewport.Height / 2 + 110),
                                        Color.White,
                                        0,
                                        Vector2.Zero,
                                        1,
                                        SpriteEffects.None,
                                        1f);

            spriteBatch.DrawString(text1, nek.leavingBees,
                                        new Vector2(100, graphics.GraphicsDevice.Viewport.Height / 2 + 140),
                                        Color.White,
                                        0,
                                        Vector2.Zero,
                                        1,
                                        SpriteEffects.None,
                                        1f);

            // Mined nektar
            spriteBatch.DrawString(text1, nek.minedNektar,
                                    new Vector2(100, graphics.GraphicsDevice.Viewport.Height / 2 + 170),
                                    Color.White,
                                    0,
                                    Vector2.Zero,
                                    1,
                                    SpriteEffects.None,
                                    1f);

            // Remaining nektar
            spriteBatch.DrawString(text1, nek.remainingNektar,
                                    new Vector2(100, graphics.GraphicsDevice.Viewport.Height / 2 + 200),
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
