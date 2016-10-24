using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OneWayOut.Components;

using OneWayOut.Manager;
using System.Collections.Generic;

namespace OneWayOut.Scenes
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Objects from all components and managers
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        Texture2D spriteSheet;

        Player player;

        Rectangle healthSize;

        KeyboardState kbState;

        KeyboardState previousKbState;

        Texture2D health;

        Texture2D signPicture;

        Texture2D collision;

        Arrow arrow;

        AssetManager asset;

        GameManager game;

        BgmManager bgm;

        BackgroundManager background;

        ForegroundTextManager foregroundText;

        Highscore highscoreText;

        bool scoreChecked;

        bool arrowExist;

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

            arrowExist = false;

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

            bgm = new BgmManager(Content);

            highscoreText = new Highscore(Content);

            player = new Player(spriteSheet);

            player.SetPositionCenter(GraphicsDevice);

            collision = Content.Load<Texture2D>(@"textures/Simple_Rectangle_-_Semi-Transparent");

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
                //START case: sets up the screen to switch between the GAME, HELP, OPTIONS screens
                case GameState.START:
                    bgm.PlayMenu();

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

                //HELP case: gives background of the game as well as instructions to play the game
                case GameState.HELP:
                    bgm.PlayHelp();

                    if (SingleKeyPress(Keys.H))
                    {
                        game.state = GameState.START;
                    }
                    break;

                //GAME case: where the game is actually played and score is gathered
                case GameState.GAME:
                    bgm.PlayGame();

                    highscoreText.getScore(player.score);

                    player.Move();

                    healthSize = new Rectangle(5, 5, player.health, 30);
                    
                    //Arrows: can be ONLY when facing left or right
                    if (player.arrowSupply > 0)
                    {
                        if (kbState.IsKeyDown(Keys.Space) && arrowExist == false)
                        {
                            int arrowX = player.position.X + 100;
                            int arrowY = player.position.Y + 40;

                            if (player.direction == Direction.RIGHT)
                            {
                                arrow = new Arrow(100, asset.arrowTexture, arrowX, arrowY);

                                arrowExist = true;

                                arrow.Collision(asset.slimes);

                                player.timer = 0;

                                player.UseArrow();
                            }

                            if (player.direction == Direction.LEFT)
                            {
                                arrowX -= 200;

                                arrow = new Arrow(100, asset.arrowTexture, arrowX, arrowY);

                                arrowExist = true;

                                arrow.Collision(asset.slimes);

                                player.timer = 0;

                                player.UseArrow();
                            }
                        }

                        if (player.timer > 60)
                        {
                            arrowExist = false;
                            player.timer = 0;
                        }
                    }

                    player.Update(gameTime);

                    game.ScreenWrap(GraphicsDevice, player);

                    for (int i = 0; i < asset.slimes.Count; i++)
                    {
                        var slime = asset.slimes[i];
                        slime.Chase(player, gameTime);
                        game.ScreenWrap(GraphicsDevice, slime);
                        //slime.SlimeAttack(player);

                        //handles when the slime dies
                        if (slime.Health <= 0)
                        {
                            player.GainArrow();

                            player.score += 50;

                            slime.IsActive = false;

                            asset.slimes.RemoveAt(i);  //removes the slime that was hit by projectile and gives play 'x' amount of arrows           
                        }
                    }

                    if (SingleKeyPress(Keys.P))
                    {
                        game.state = GameState.PAUSE;
                    }

                    if (SingleKeyPress(Keys.Z) || player.health == 0)
                    {
                        game.state = GameState.GAMEOVER;
                    }

                    break;

                //OPTIONS case: will display the sound options, etc.
                case GameState.OPTIONS:

                    bgm.PlayOptions();

                    if (SingleKeyPress(Keys.O))
                    {
                        game.state = GameState.START;
                    }
                    break;

                //GAME OVER case: displays the highscores for the players and gives the options to go back to GAME or START
                case GameState.GAMEOVER:

                    bgm.PlayGameOver();

                    if (SingleKeyPress(Keys.G))
                    {
                        game.state = GameState.GAME;
                    }

                    if (SingleKeyPress(Keys.Enter))
                    {
                        game.state = GameState.START;
                    }
                    break;

                //PAUSE case: stops all movement and music in-game
                case GameState.PAUSE:

                    bgm.Pause();

                    if (SingleKeyPress(Keys.P))
                    {
                        game.state = GameState.GAME;
                        bgm.Resume();
                    }

                    if (SingleKeyPress(Keys.Q))
                    {
                        game.state = GameState.GAMEOVER;
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
                    Rectangle playerBounds = new Rectangle(player.position.X, player.position.Y, player.position.Width, player.position.Height);
                    //Rectangle arrowBounds = new Rectangle(arrow.position.X, arrow.position.Y, arrow.position.Width, arrow.position.Height);
                    
                    asset.DrawDungeon(spriteBatch);

                    scoreChecked = false;

                    spriteBatch.Draw(health, new Rectangle(4, 5, 102, 31), Color.Black);

                    spriteBatch.Draw(health, healthSize, Color.White);

                    highscoreText.DrawScore(spriteBatch, player);

                    player.Draw(spriteBatch, new Vector2(200, 50));
                    spriteBatch.Draw(collision, playerBounds, Color.Red);

                    if (arrowExist == true && player.arrowSupply > 0)
                    {
                        if (IsActive)
                        {
                            if (player.direction == Direction.RIGHT)
                            {
                                spriteBatch.Draw(asset.arrowTexture, arrow.position, Color.White);
                                //spriteBatch.Draw(collision, arrowBounds, Color.Red);
                            }
                            if (player.direction == Direction.LEFT)
                            {
                                spriteBatch.Draw(asset.arrowTexture, arrow.position, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                                //spriteBatch.Draw(collision, arrowBounds, Color.Red);
                            }
                        }
                    }

                    asset.DrawSlimes(spriteBatch, foregroundText);

                    foregroundText.DrawGame(spriteBatch, player);

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

                    foregroundText.DrawGameover(spriteBatch);

                    highscoreText.readScore();

                    if (scoreChecked == false)
                    {
                        highscoreText.CheckScore(player.score);
                        scoreChecked = true;
                    }

                    highscoreText.DrawScore(spriteBatch);
                    break;

                //Draw Pause
                case GameState.PAUSE:

                    asset.DrawDungeon(spriteBatch);

                    spriteBatch.Draw(health, new Rectangle(4, 5, 152, 31), Color.Black);
                    spriteBatch.Draw(health, new Rectangle(5, 5, 150, 30), Color.White);

                    player.Draw(spriteBatch, new Vector2(200, 50));

                    asset.DrawSlimes(spriteBatch, foregroundText);

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