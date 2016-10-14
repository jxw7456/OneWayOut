using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using OneWayOut.Components.Player;
using OneWayOut.Components.Arrow;
using OneWayOut.Manager;

namespace OneWayOut.Scenes
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        Texture2D spriteSheet;

        Player MC;

        KeyboardState kbState;
        KeyboardState previousKbState;

        Texture2D health;

        Texture2D signPicture;

        AssetManager asset;

        GameManager game;

        Song currentSong, menuSong, helpSong, gameSong, gameOverSong, optionsSong;

        BackgroundManager background;

        ForegroundTextManager foregroundText;

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

            game = new GameManager();

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

            signPicture = Content.Load<Texture2D>(@"textures/signlanguage");

            spriteSheet = Content.Load<Texture2D>(@"textures/ArcherSpritesheet");

            health = Content.Load<Texture2D>(@"textures/health");

            asset = new AssetManager(Content, GraphicsDevice);

            background = new BackgroundManager(Content);

            foregroundText = new ForegroundTextManager(Content);

            //Songs for all screens
            menuSong = Content.Load<Song>(@"media/menu");            
            gameSong = Content.Load<Song>(@"media/game");
            gameOverSong = Content.Load<Song>(@"media/gameOver");
            optionsSong = Content.Load<Song>(@"media/options");
            helpSong = Content.Load<Song>(@"media/help");

            currentSong = menuSong;
            MediaPlayer.Volume = 0.50f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(currentSong);

            MC = new Player(spriteSheet, 1, 4);

            MC.SetPositionCenter(GraphicsDevice);

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

            switch (game.state)
            {
                //START case
                case GameState.START:
                    if(!MediaPlayer.Equals(currentSong, menuSong))
                    {
                        currentSong = menuSong;
                        MediaPlayer.Play(currentSong);
                    }

                    if (SingleKeyPress(Keys.Enter))
                    {
                        game.state = GameState.GAME;
                    }

                    if (SingleKeyPress(Keys.H))
                    {
                        game.state = GameState.HELP;
                    }

                    if (SingleKeyPress(Keys.O))
                    {
                        game.state = GameState.OPTIONS;
                    }
                    break;

                //HELP case
                case GameState.HELP:
                    if (!MediaPlayer.Equals(currentSong, helpSong))
                    {
                        currentSong = helpSong;
                        MediaPlayer.Play(currentSong);
                    }

                    if (SingleKeyPress(Keys.H))
                    {
                        game.state = GameState.START;
                    }
                    break;

                //GAME case
                case GameState.GAME:
                    if (!MediaPlayer.Equals(currentSong, gameSong))
                    {
                        currentSong = gameSong;
                        MediaPlayer.Play(currentSong);
                    }

                    MC.Move();
                    MC.Update(gameTime);

                    if (SingleKeyPress(Keys.P))
                    {
                        game.state = GameState.PAUSE;
                        MediaPlayer.Pause();
                    }
                    if (SingleKeyPress(Keys.Z))
                    {
                        game.state = GameState.GAMEOVER;
                    }

                    game.ScreenWrap(GraphicsDevice, asset.slime);

                    game.ScreenWrap(GraphicsDevice, MC);

                    asset.slime.ProcessInput(gameTime);
                    break;

                //OPTIONS case
                case GameState.OPTIONS:
                    if (!MediaPlayer.Equals(currentSong, optionsSong))
                    {
                        currentSong = optionsSong;
                        MediaPlayer.Play(currentSong);
                    }

                    if (SingleKeyPress(Keys.O))
                    {
                        game.state = GameState.START;
                    }
                    break;

                //GAME OVER case
                case GameState.GAMEOVER:
                    if (!MediaPlayer.Equals(currentSong, gameOverSong))
                    {
                        currentSong = gameOverSong;
                        MediaPlayer.Play(currentSong);
                    }

                    if (SingleKeyPress(Keys.G))
                    {
                        game.state = GameState.GAME;
                    }

                    if (SingleKeyPress(Keys.Enter))
                    {
                        game.state = GameState.START;
                    }
                    break;

                //PAUSE case
                case GameState.PAUSE:
                    MediaPlayer.Pause();

                    if (SingleKeyPress(Keys.P))
                    {
                        game.state = GameState.GAME;
                        MediaPlayer.Resume();
                    }

                    if (SingleKeyPress(Keys.Q))
                    {
                        game.state = GameState.START;
                    }
                    break;
            }

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

            switch (game.state)
            {
                //Draw Menu
                case GameState.START:
                    background.DrawStart(spriteBatch, GraphicsDevice);

                    foregroundText.DrawStart(spriteBatch);
                    break;

                //Draw Game
                case GameState.GAME:
                    asset.dungeon.Draw(spriteBatch);
                    spriteBatch.Draw(health, new Rectangle(4, 5, 152, 31), Color.Black);
                    spriteBatch.Draw(health, new Rectangle(5, 5, 150, 30), Color.White);

                    MC.Draw(spriteBatch, new Vector2(200, 50));

                    asset.slime.Draw(spriteBatch);
                    break;

                //Draw Help
                case GameState.HELP:
                    background.DrawHelp(spriteBatch, GraphicsDevice);

                    spriteBatch.Draw(signPicture, new Rectangle(300, 290, 200, 180), Color.White);

                    foregroundText.DrawHelp(spriteBatch);
                    break;

                //Draw Options
                case GameState.OPTIONS:
                    background.DrawOption(spriteBatch, GraphicsDevice);

                    break;

                //Draw Game Over
                case GameState.GAMEOVER:
                    background.DrawGameover(spriteBatch, GraphicsDevice);

                    foregroundText.DrawOption(spriteBatch);
                    break;

                //Draw Pause
                case GameState.PAUSE:
                    asset.dungeon.Draw(spriteBatch);
                    spriteBatch.Draw(health, new Rectangle(4, 5, 152, 31), Color.Black);
                    spriteBatch.Draw(health, new Rectangle(5, 5, 150, 30), Color.White);

                    MC.Draw(spriteBatch, new Vector2(200, 50));

                    asset.slime.Draw(spriteBatch);

                    foregroundText.DrawPause(spriteBatch);
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
