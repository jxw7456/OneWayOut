using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using OneWayOut.Components;
using OneWayOut.Components.Drop;
using OneWayOut.Manager;
using System.Collections.Generic;
using System;

namespace OneWayOut.Scenes
{
    //Limit to amount that spawn on a level
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Objects from all components and managers
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        // TODO: Refactor player and all texture/rectangle into assetManager

        Player player;

        Rectangle healthSize;

        Texture2D health;

        Rectangle healthContainer;

        Arrow arrow;

        AssetManager asset;

        GameManager game;

        BgmManager bgm;

        BackgroundManager background;

        Texture2D healthPack;

        Texture2D arrowDrop;

        Drop currentItem = null;

        Drop item;

        List<Drop> allItems = new List<Drop>();

        ForegroundTextManager foregroundText;
        // TODO: Refactor, this should follow the rest of the naming
        // schema already in use.
        Highscore highscoreText;

        InputManager input;

        bool scoreChecked;

        bool checkIt = false;

        bool dropIt = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window            
            graphics.ToggleFullScreen();
            graphics.IsFullScreen = true;

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

            input = new InputManager();

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

            // TODO: Refactor this into either assetManager or 
            // some manager specialized for sprite
            health = Content.Load<Texture2D>(@"textures/health");

            healthPack = Content.Load<Texture2D>(@"textures/healthpack");

            arrowDrop = Content.Load<Texture2D>(@"textures/arrow");

            asset = new AssetManager(Content, GraphicsDevice);

            background = new BackgroundManager(Content);

            foregroundText = new ForegroundTextManager(Content);

            bgm = new BgmManager(Content);

            highscoreText = new Highscore(Content);

            player = asset.player;

            player.SetPositionCenter(GraphicsDevice);

            healthContainer = new Rectangle(4, 5, 102, 31);

            healthSize = new Rectangle(5, 5, player.Health, 30);
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

            input.Record(gameTime);

            switch (game.state)
            {
                //START case: sets up the screen to switch between the GAME, HELP, OPTIONS screens
                case GameState.START:
                    checkIt = false;
                    bgm.PlayMenu();

                    break;

                //HELP case: gives background of the game as well as instructions to play the game
                case GameState.HELP:
                    checkIt = false;
                    bgm.PlayHelp();

                    break;

                //GAME case: where the game is actually played and score is gathered
                case GameState.GAME:
                    bgm.PlayGame();

                    checkIt = false;

                    if (input.SingleKeyPress((Keys)GameState.NEXTLEVEL))
                    {
                        NextLevel();
                    }

                    highscoreText.getScore(player.Score);

                    player.Move();

                    player.Update(gameTime);

                    healthSize.Width = player.Health;

                    game.ScreenWrap(GraphicsDevice, player);

                    if (arrow != null &&
                        arrow.Target < asset.slimes.Count &&
                        asset.slimes.Count > 0)
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
                            int arrowX = player.Position.X;

                            int arrowY = player.Position.Y + 40;

                            arrow = new Arrow(100, asset.arrowTexture, arrowX, arrowY);

                            arrow.Target = i;

                            input.ClearStack();

                            player.UseArrow();
                        }

                        slime.Chase(player, gameTime);

                        game.ScreenWrap(GraphicsDevice, slime);

                        slime.Attack(player);

                        //handles when the slime dies
                        if (slime.Health <= 0)
                        {
                            //player.GainArrow();
                            item = new Drop(healthPack, arrowDrop, slime.Position.X, slime.Position.Y, 50, 50);
                            item.PickDrop();
                            dropIt = true;
                            allItems.Add(item);
                            player.Score += 50;

                            asset.slimes.RemoveAt(i);  //removes the slime that was hit by projectile and gives play 'x' amount of arrows
                        }
                    }

                    for (int i = 0; i < allItems.Count; i++)
                    {
                        item.intersection(dropIt, player, allItems, item, i);
                    }


                    if (asset.slimes.Count == 0)
                    {
                        NextLevel();
                    }

                    if (player.Health <= 0)
                    {
                        highscoreText.getScore(player.Score);
                        Reset();
                        game.state = GameState.GAMEOVER;                        
                    }
                    break;

                //OPTIONS case: will display the sound options, etc.
                case GameState.STORY:

                    bgm.PlayStory();
                    //Code for changing volume and putting it in the options screen                  
                    //No need to check if a boolean is true or false
                    //yes there is. The exe will continuesly pop up and you cant exit it.
                    if (checkIt == false)
                    {
                        checkIt = true;

                        try
                        {
                            /*
                            Process firstProc = new Process();
                            firstProc.StartInfo.FileName = "..\\..\\..\\..\\..\\one way outexternal tool.exe";
                            firstProc.EnableRaisingEvents = true;
                            
                            firstProc.Start();

                            firstProc.WaitForExit();
                            */
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    break;

                //GAME OVER case: displays the highscores for the players and gives the options to go back to GAME or START
                case GameState.GAMEOVER:
                    checkIt = false;
                    bgm.PlayGameOver();

                    if (input.SingleKeyPress((Keys)GameState.GAME) || input.SingleKeyPress((Keys)GameState.START))
                    {
                        Reset();
                    }

                    break;

                //PAUSE case: stops all movement and music in-game
                case GameState.PAUSE:
                    checkIt = false;
                    bgm.Pause();

                    if (input.SingleKeyPress((Keys)GameState.START))
                    {
                        Reset();
                    }

                    break;
            }

            input.SwitchScene(game, bgm);

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

                    spriteBatch.Draw(health, healthContainer, Color.Black);

                    spriteBatch.Draw(health, healthSize, Color.White);

                    if (dropIt == true)
                    {
                        for (int i = 0; i < allItems.Count; i++)
                        {
                            item = allItems[i];
                            item.DrawDrop(spriteBatch);
                        }

                    }

                    highscoreText.DrawScore(spriteBatch, player);

                    player.Draw(spriteBatch);

                    if (arrow != null)
                    {
                        arrow.Draw(spriteBatch, gameTime);
                    }

                    asset.DrawSlimes(spriteBatch, foregroundText);

                    foregroundText.DrawGame(spriteBatch, player);

                    //DEBUG
                    foregroundText.DrawDebug(spriteBatch, input.TypingStack);

                    break;

                //Draw Help
                case GameState.HELP:

                    background.DrawHelp(spriteBatch, GraphicsDevice);

                    foregroundText.DrawHelp(spriteBatch);

                    break;

                //Draw Options
                case GameState.STORY:

                    background.DrawStory(spriteBatch, GraphicsDevice);

                    foregroundText.DrawStory(spriteBatch);

                    break;

                //Draw Game Over
                case GameState.GAMEOVER:

                    background.DrawGameover(spriteBatch, GraphicsDevice);

                    foregroundText.DrawGameover(spriteBatch);

                    // TODO: Refactor, this logic should happen in the Update if it ever needed to.
                    // Also there is a way to do this without having a check.
                    // Give it some thought and if stuck feel free to ask lab

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

                    // TODO: Refactor, this can be done better by caching the  rectangle
                    spriteBatch.Draw(health, new Rectangle(4, 5, 102, 31), Color.Black);

                    spriteBatch.Draw(health, healthSize, Color.White);

                    player.Draw(spriteBatch);

                    highscoreText.DrawScore(spriteBatch, player);

                    asset.DrawSlimes(spriteBatch, foregroundText);

                    foregroundText.DrawPause(spriteBatch, player);

                    break;

                case GameState.CREDITS:

                    background.DrawStart(spriteBatch, GraphicsDevice);

                    foregroundText.DrawCredits(spriteBatch);

                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void Reset()
        {
            asset.ResetGame(GraphicsDevice);

            game.Reset();
        }

        void NextLevel()
        {
            game.NextLevel();

            asset.Clear();

            asset.NextLevel(GraphicsDevice);

            player.SetPositionCenter(GraphicsDevice);
        }
    }
}