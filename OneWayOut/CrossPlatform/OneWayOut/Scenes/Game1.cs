using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using OneWayOut.Components.Player;
using OneWayOut.Components.Arrow;
using OneWayOut.Manager;
using System;

namespace OneWayOut
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

        GameState state;

        AssetManager asset;

        Song currentSong, menuSong, helpSong;

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

            asset = new AssetManager(Content);

            //menu song
            menuSong = Content.Load<Song>(@"media/menu");
            currentSong = menuSong;
            //help song
            helpSong = Content.Load<Song>(@"media/help");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.50f;
            MediaPlayer.Play(currentSong);

            background = new BackgroundManager(Content);

            foregroundText = new ForegroundTextManager(Content);

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
                    //Play Menu Background Music
                    if (!MediaPlayer.Equals(currentSong, menuSong))
                    {
                        currentSong = menuSong;
                        MediaPlayer.Play(currentSong);
                    }

                    if (SingleKeyPress(Keys.Enter))
                    {
                        state = GameState.GAME;
                    }

                    if (SingleKeyPress(Keys.H))
                    {
                        state = GameState.HELP;
                    }

                    if (SingleKeyPress(Keys.O))
                    {
                        state = GameState.OPTIONS;
                    }
                    break;

                //HELP case
                case GameState.HELP:
                    //Play Help Background Music
                    if (!MediaPlayer.Equals(currentSong, helpSong))
                    {
                        currentSong = helpSong;
                        MediaPlayer.Play(currentSong);
                    }

                    if (SingleKeyPress(Keys.H))
                    {
                        state = GameState.START;
                    }
                    break;

                //GAME case
                case GameState.GAME:
                    MediaPlayer.Stop();

                    MC.Move();
                    MC.Update(gameTime);

                    if (SingleKeyPress(Keys.P))
                    {
                        state = GameState.PAUSE;
                        MediaPlayer.Pause();
                    }
                    if (SingleKeyPress(Keys.Z))
                    {
                        state = GameState.GAMEOVER;
                    }
                    break;

                //OPTIONS case
                case GameState.OPTIONS:
                    MediaPlayer.Stop();

                    if (SingleKeyPress(Keys.O))
                    {
                        state = GameState.START;
                    }
                    break;

                //GAME OVER case
                case GameState.GAMEOVER:
                    MediaPlayer.Stop();

                    if (SingleKeyPress(Keys.G))
                    {
                        state = GameState.GAME;
                    }

                    if (SingleKeyPress(Keys.Enter))
                    {
                        state = GameState.START;
                    }
                    break;

                //PAUSE case
                case GameState.PAUSE:
                    MediaPlayer.Stop();

                    if (SingleKeyPress(Keys.P))
                    {
                        state = GameState.GAME;
                        MediaPlayer.Resume();
                    }

                    if (SingleKeyPress(Keys.Q))
                    {
                        state = GameState.START;
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

            switch (state)
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
