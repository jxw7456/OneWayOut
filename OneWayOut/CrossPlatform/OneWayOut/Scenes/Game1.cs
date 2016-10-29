using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using OneWayOut.Components;

using OneWayOut.Manager;
using System.Collections.Generic;
using System;

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

        Texture2D health;

        Texture2D signPicture;

        Arrow arrow;

        AssetManager asset;

        GameManager game;

        BgmManager bgm;

        BackgroundManager background;

        ForegroundTextManager foregroundText;

        Highscore highscoreText;

        InputManager input;

        bool scoreChecked;

        bool checkIt = false;

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

            bgm = new BgmManager(Content);

            highscoreText = new Highscore(Content);

            input = new InputManager();

            player = new Player(spriteSheet);

            player.SetPositionCenter(GraphicsDevice);

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

            input.CacheKeyboardState();

            input.SwitchScene(game, bgm);

            input.Record(gameTime);

            switch (game.state)
            {
            //START case: sets up the screen to switch between the GAME, HELP, OPTIONS screens
                case GameState.START:
                    bgm.PlayMenu();

                    break;

            //HELP case: gives background of the game as well as instructions to play the game
                case GameState.HELP:
                    bgm.PlayHelp();

                    break;

            //GAME case: where the game is actually played and score is gathered
                case GameState.GAME:
                    bgm.PlayGame();

                    checkIt = false;

                    highscoreText.getScore(player.Score);

                    player.Move();

                    player.Update(gameTime);

                    healthSize = new Rectangle(5, 5, player.Health, 30);

                    game.ScreenWrap(GraphicsDevice, player);

                    if (arrow != null && asset.slimes.Count > 0)
                    {
                        var targetSlime = asset.slimes[arrow.Target];
                        
                        arrow.Move(targetSlime);

                        arrow.Collision(asset.slimes);
                    }
                        
                    for (int i = 0; i < asset.slimes.Count; i++)
                    {
                        var slime = asset.slimes[i];

                        if (slime.CompareName(input.TypingStack) && player.ArrowCount > 0)
                        {
                            player.UseArrow();

                            int arrowX = player.Position.X;

                            int arrowY = player.Position.Y + 40;

                            arrow = new Arrow(100, asset.arrowTexture, arrowX, arrowY);

                            arrow.Target = i;                       
                        }

                        slime.Chase(player, gameTime);

                        game.ScreenWrap(GraphicsDevice, slime);

                        slime.Attack(player);

                        //handles when the slime dies
                        if (slime.Health <= 0)
                        {
                            player.GainArrow();

                            player.Score += 50;

                            asset.slimes.RemoveAt(i);  //removes the slime that was hit by projectile and gives play 'x' amount of arrows
                        }
                    }

                    if (player.Health <= 0)
                    {
                        game.state = GameState.GAMEOVER;
                        highscoreText.getScore(player.Score);
                        ResetGame();
                    }

                    break;

            //OPTIONS case: will display the sound options, etc.
                case GameState.OPTIONS:

                    bgm.PlayOptions();
                    //TODO
                    //Code for changing volume and putting it in the options screen
                    // Process firstProc = new Process();
                    if (checkIt == false)
                    {
                        checkIt = true;
                        try
                        {

                            Process firstProc = new Process();
                            firstProc.StartInfo.FileName = "one way outexternal tool.exe";
                            firstProc.EnableRaisingEvents = true;

                            firstProc.Start();

                            firstProc.WaitForExit();

                        }
                        catch (Exception ex)
                        {

                        }


                    }

                    break;

            //GAME OVER case: displays the highscores for the players and gives the options to go back to GAME or START
                case GameState.GAMEOVER:

                    bgm.PlayGameOver();


                    break;

            //PAUSE case: stops all movement and music in-game
                case GameState.PAUSE:

                    bgm.Pause();

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
            GraphicsDevice.Clear(Color.Black);

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

                    asset.DrawDungeon(spriteBatch);

                    scoreChecked = false;

                    spriteBatch.Draw(health, new Rectangle(4, 5, 102, 31), Color.Black);

                    spriteBatch.Draw(health, healthSize, Color.White);

                    highscoreText.DrawScore(spriteBatch, player);

                    player.Draw(spriteBatch);

                    if (arrow != null)
                    {
                        arrow.Draw(spriteBatch, gameTime);
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

                    foregroundText.DrawOption(spriteBatch);

                    break;

            //Draw Game Over
                case GameState.GAMEOVER:

                    background.DrawGameover(spriteBatch, GraphicsDevice);

                    foregroundText.DrawGameover(spriteBatch);

                    highscoreText.readScore();

                    if (scoreChecked == false)
                    {
                        highscoreText.CheckScore();
                        scoreChecked = true;
                    }

                    highscoreText.DrawScore(spriteBatch);
                    break;

            //Draw Pause
                case GameState.PAUSE:

                    asset.DrawDungeon(spriteBatch);

                    spriteBatch.Draw(health, new Rectangle(4, 5, 102, 31), Color.Black);

                    spriteBatch.Draw(health, healthSize, Color.White);

                    player.Draw(spriteBatch);

                    highscoreText.DrawScore(spriteBatch, player);

                    asset.DrawSlimes(spriteBatch, foregroundText);

                    foregroundText.DrawPause(spriteBatch);

                    break;
            }

            // DEBUG:

            foregroundText.DrawDebug(spriteBatch, input.TypingStack);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //Resets the game if player dies or quits
        public void ResetGame()
        {
            player.Health = 100;
            player.Score = 0;
            player.ArrowCount = 50;
            player.SetPositionCenter(GraphicsDevice);
            //add new slime for the player            
        }

        //Draws new slime after clearing out all slime
        public void NextWave()
        {
            //do what is done in the above method
        }
    }
}