using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OneWayOut.Components.Player;
using OneWayOut.Components.Arrow;

namespace OneWayOut
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Texture2D mainArcher;
        Player MC = new Player();
        KeyboardState kbState;
        KeyboardState previousKbState;
        Menues menuState;
        public int screenWidth;
        public int screenHeight;

        enum Menues{
            START,
            HELP,
            GAME,
            OPTIONS,
            GAMEOVER,
            PAUSE
        }
       
        public Game1()
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
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

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
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");

            mainArcher = Content.Load<Texture2D>("archer.png");
            // TODO: use this.Content to load your game content here
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

            // TODO: Add your update logic here
            string strState = "";
            previousKbState = kbState;
            kbState = Keyboard.GetState();

            /*START,
            HELP,
            GAME,
            OPTIONS,
            GAMEOVER,
            PAUSE
            */
            if (menuState == Menues.START)
            {
                strState = "START";
            }

            if (menuState == Menues.HELP)
            {
                strState = "HELP";
            }

            if (menuState == Menues.GAME)
            {
                strState = "GAME";
            }

            if (menuState == Menues.OPTIONS)
            {
                strState = "OPTIONS";
            }

            if (menuState == Menues.GAMEOVER)
            {
                strState = "GAMEOVER";
            }

            if (menuState == Menues.PAUSE)
            {
                strState = "PAUSE";
            }

            switch (strState)
            {
                case "START":
                    if (SingleKeyPress(Keys.Enter) == true)
                    {

                    }
                    break;
            }

            MC.move();

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

            //Draw Menu
            if (menuState == Menues.START)
            {
                spriteBatch.DrawString(spriteFont, "One Way Out", new Vector2(screenWidth / 2, screenHeight / 2), Color.DarkRed);
                spriteBatch.DrawString(spriteFont, "Press Enter to Start", new Vector2(screenWidth / 2, (screenHeight / 2) + 20), Color.Red);
            }

            spriteBatch.Draw(mainArcher, MC.archerlocal, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Returns a bool for a key press
        public bool SingleKeyPress(Keys keys)
        {
            bool valid = false;

            if (kbState.IsKeyDown(keys) == true)
            {
                if (previousKbState.IsKeyUp(keys) == true)
                {
                    valid = true;
                }
            }
            return valid;
        }
    }
}
