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

        AssetManager asset;

        GameManager game;

        BgmManager bgm;

        BackgroundManager background;        

        Drop currentItem = null;

        Drop item;

        List<Drop> allItems = new List<Drop>();

        ForegroundTextManager foregroundText;

        Highscore highscoreText;

        InputManager input;        

        bool scoreChecked;

        bool checkIt = false;

        bool dropIt = false;

        bool drawit = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window            
            //graphics.ToggleFullScreen();
            //graphics.IsFullScreen = true;

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

            asset = new AssetManager(Content, GraphicsDevice);

            background = new BackgroundManager(Content);

            foregroundText = new ForegroundTextManager(Content);

            bgm = new BgmManager(Content);

            highscoreText = new Highscore(Content);

            player = asset.player;

            player.SetPositionCenter(GraphicsDevice);            
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

                    if (checkIt == false && drawit == false && input.SingleKeyPress(Keys.F7))
                    {
                        checkIt = true;
                        drawit = true;
                    }

                    if (checkIt == false && drawit == true && input.SingleKeyPress(Keys.F7))
                    {
                        checkIt = true;
                        drawit = false;
                    }

                    player.Move();

                    player.Update(gameTime);

                    asset.healthSize.Width = player.Health;

                    game.ScreenWrap(GraphicsDevice, player);

                    if (asset.arrow != null && asset.arrow.Target < asset.slimes.Count && asset.slimes.Count > 0)
                    {
                        var targetSlime = asset.slimes[asset.arrow.Target];

                        asset.arrow.Move(targetSlime);

                        asset.arrow.Collision(asset.slimes);
                    }

                    for (int i = 0; i < asset.slimes.Count; i++)
                    {
                        var slime = asset.slimes[i];

                        if (slime.CompareName(input.TypingStack) && player.ArrowCount > 0)
                        {
                            int arrowX = player.Position.X;

                            int arrowY = player.Position.Y + 40;

                            asset.arrow = new Arrow(100, asset.arrowTexture, arrowX, arrowY);

                            asset.arrow.Target = i;

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
                            item = new Drop(asset.healthPack, asset.arrowDrop, slime.Position.X, slime.Position.Y, 50, 50);
                            item.PickDrop();
                            dropIt = true;
                            allItems.Add(item);
                            player.Score += 50;
                            
                            //removes the slime that was hit by projectile and gives play 'x' amount of arrows
                            asset.slimes.RemoveAt(i);  
                        }
                    }

                    for (int i = 0; i < allItems.Count; i++)
                    {
                        item.Intersection(dropIt, player, allItems, item, i);
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

                //STORY case: will display the sound options, etc.
                case GameState.STORY:
                    bgm.PlayStory();

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
                    if (checkIt == false && drawit == false && input.SingleKeyPress(Keys.F7))
                    {
                        checkIt = true;
                        drawit = true;
                    }
                    if (checkIt == false && drawit == true && input.SingleKeyPress(Keys.F7))
                    {
                        checkIt = true;
                        drawit = false;
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

                    spriteBatch.Draw(asset.health, asset.healthContainer, Color.Black);

                    spriteBatch.Draw(asset.health, asset.healthSize, Color.White);

                    if (dropIt == true)
                    {
                        for (int i = 0; i < allItems.Count; i++)
                        {
                            item = allItems[i];
                            item.DrawDrop(spriteBatch);
                        }
                    }

                    if (drawit == true)
                    {
                        spriteBatch.Draw(asset.signlanguage, new Rectangle(1620, 730, 300, 300), Color.RoyalBlue);
                    }

                    highscoreText.DrawScore(spriteBatch, player);

                    player.Draw(spriteBatch);

                    if (asset.arrow != null)
                    {
                        asset.arrow.Draw(spriteBatch, gameTime);
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

                    if (drawit == true)
                    {
                        spriteBatch.Draw(asset.signlanguage, new Rectangle(1620, 730, 300, 300), Color.RoyalBlue);
                    }

                    // TODO: Refactor, this can be done better by caching the  rectangle
                    spriteBatch.Draw(asset.health, new Rectangle(4, 5, 102, 31), Color.Black);

                    spriteBatch.Draw(asset.health, asset.healthSize, Color.White);

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