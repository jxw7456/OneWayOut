using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OneWayOut.Components.Player;
using OneWayOut.Components.Arrow;
using OneWayOut.Manager;

namespace OneWayOut
{
    enum GameState
    {
        START,
        HELP,
        GAME,
        OPTIONS,
        GAMEOVER,
        PAUSE
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont1;
        SpriteFont spriteFont2;
        Texture2D spriteSheet;
        Texture2D background;
        Texture2D health;
        Player MC;
        KeyboardState kbState;
        KeyboardState previousKbState;

        GameState state;

        public int screenWidth;
        public int screenHeight;
        AssetManager asset;

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

            spriteFont1 = Content.Load<SpriteFont>(@"fonts/bold");
            spriteFont2 = Content.Load<SpriteFont>(@"fonts/biggerFont");
            background = Content.Load<Texture2D>(@"textures/dungeon");
            spriteSheet = Content.Load<Texture2D>(@"textures/ArcherSpritesheet");
            health = Content.Load<Texture2D>(@"textures/health");

            asset = new AssetManager(Content);
            MC = new Player(spriteSheet, 1, 4);            
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
            previousKbState = kbState;
            kbState = Keyboard.GetState();

            switch (state)
            {
                //START case
                case GameState.START:
                    if (SingleKeyPress(Keys.Enter) == true)
                    {
                        state = GameState.GAME;
                    }

                    if (SingleKeyPress(Keys.H) == true)
                    {
                        state = GameState.HELP;
                    }

                    if (SingleKeyPress(Keys.O) == true)
                    {
                        state = GameState.OPTIONS;
                    }
                    break;

                //HELP case
                case GameState.HELP:
                    if (SingleKeyPress(Keys.H) == true)
                    {
                        state = GameState.START;
                    }
                    break;

                //GAME case
                case GameState.GAME:
                    MC.Update(gameTime);
                    if (SingleKeyPress(Keys.P) == true)
                    {
                        state = GameState.PAUSE;
                    }
                    if (SingleKeyPress(Keys.Z) == true)
                    {
                        state = GameState.GAMEOVER;
                    }
                    break;

                //OPTIONS case
                case GameState.OPTIONS:
                    if (SingleKeyPress(Keys.O) == true)
                    {
                        state = GameState.START;
                    }
                    break;

                //GAME OVER case
                case GameState.GAMEOVER:
                    if (SingleKeyPress(Keys.G) == true)
                    {
                        state = GameState.GAME;
                    }

                    if (SingleKeyPress(Keys.Enter) == true)
                    {
                        state = GameState.START;
                    }
                    break;
                
                //PAUSE case
                case GameState.PAUSE:
                    if (SingleKeyPress(Keys.P) == true)
                    {
                        state = GameState.GAME;
                    }

                    if (SingleKeyPress(Keys.Q) == true)
                    {
                        state = GameState.START;
                    }
                    break;
            }

            MC.Move();

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

            switch (state)
            {
                //Draw Menu
                case GameState.START:
                    background = Content.Load<Texture2D>(@"textures/dungeon");
                    spriteBatch.Draw(background, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);                    
                    spriteBatch.DrawString(spriteFont2, "One Way Out", new Vector2(225, 10), Color.White);
                    spriteBatch.DrawString(spriteFont1, "Press 'Enter' to Start", new Vector2(235, 180), Color.OrangeRed);
                    spriteBatch.DrawString(spriteFont1, "Press 'H' for Help", new Vector2(270, 210), Color.OrangeRed);
                    spriteBatch.DrawString(spriteFont1, "Press 'O' for Options", new Vector2(240, 240), Color.OrangeRed);                    
                    spriteBatch.DrawString(spriteFont1, "Press 'Esc' to Quit", new Vector2(255, 440), Color.Red);
                    break;

                //Draw Game
                case GameState.GAME:
                    asset.dungeon.Draw(spriteBatch);
                    spriteBatch.Draw(health, new Rectangle(4, 5, 152, 31), Color.Black);
                    spriteBatch.Draw(health, new Rectangle(5, 5,150,30), Color.White);
                    
                    MC.Draw(spriteBatch, new Vector2(200, 50));
                    break;

                //Draw Help
                case GameState.HELP:
                    background = Content.Load<Texture2D>(@"textures/dungeonHelp");
                    spriteBatch.Draw(background, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    break;

                //Draw Options
                case GameState.OPTIONS:
                    background = Content.Load<Texture2D>(@"textures/dungeonOptions");
                    spriteBatch.Draw(background, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);                    
                    break;

                //Draw Game Over
                case GameState.GAMEOVER:
                    background = Content.Load<Texture2D>(@"textures/deadArcher");
                    spriteBatch.Draw(background, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    spriteBatch.DrawString(spriteFont2, "YOU ARE DEAD", new Vector2(225, 10), Color.Red);
                    spriteBatch.DrawString(spriteFont1, "Press 'G' to Restart", new Vector2(270, 410), Color.White);
                    spriteBatch.DrawString(spriteFont1, "Press 'Enter' for Main Menu", new Vector2(225, 440), Color.White);                    
                    break;

                //Draw Pause
                case GameState.PAUSE:
                    
                    break;
            }            

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
