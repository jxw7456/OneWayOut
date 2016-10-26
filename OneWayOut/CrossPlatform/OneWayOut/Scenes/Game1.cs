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

		KeyboardState kbState;

		KeyboardState previousKbState;

		Texture2D health;

		Texture2D signPicture;

		Arrow arrow;

		AssetManager asset;

		GameManager game;

		BgmManager bgm;

		BackgroundManager background;

		ForegroundTextManager foregroundText;

		Highscore highscoreText;

		bool scoreChecked;

		bool arrowExist;

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);

			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here

			game = new GameManager ();

			arrowExist = false;

			base.Initialize ();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			signPicture = Content.Load<Texture2D> (@"textures/signlanguage");

			spriteSheet = Content.Load<Texture2D> (@"textures/ArcherSpritesheet");

			health = Content.Load<Texture2D> (@"textures/health");

			asset = new AssetManager (Content, GraphicsDevice);

			background = new BackgroundManager (Content);

			foregroundText = new ForegroundTextManager (Content);

			bgm = new BgmManager (Content);

			highscoreText = new Highscore (Content);

			player = new Player (spriteSheet);

			arrow = new Arrow (100, asset.arrowTexture, player.position.X + 100, player.position.Y + 40);

			player.SetPositionCenter (GraphicsDevice);

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent ()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();

			// TODO: Add your update logic here
			previousKbState = kbState;

			kbState = Keyboard.GetState ();

			// Loop through all the enum to check for clicked state instead of going through each individually
			foreach (GameState state in Enum.GetValues(typeof(GameState))) {
				if (SingleKeyPress ((Keys)state)) {
					Console.WriteLine (state);

					game.state = state;

					if (state == GameState.GAME) {
						bgm.Resume ();
					}
				}
			}


			switch (game.state) {
			//START case: sets up the screen to switch between the GAME, HELP, OPTIONS screens
			case GameState.START:
				bgm.PlayMenu ();

				break;

			//HELP case: gives background of the game as well as instructions to play the game
			case GameState.HELP:
				bgm.PlayHelp ();

				break;

			//GAME case: where the game is actually played and score is gathered
			case GameState.GAME:
				bgm.PlayGame ();

				highscoreText.getScore (player.score);

				player.Move ();

				player.Update (gameTime);

				healthSize = new Rectangle (5, 5, player.health, 30);

				game.ScreenWrap (GraphicsDevice, player);

                    //TODO
                    //arrow.Move(player.direction);

				for (int i = 0; i < asset.slimes.Count; i++) {
					var slime = asset.slimes [i];
					slime.Chase (player, gameTime);
					game.ScreenWrap (GraphicsDevice, slime);
					slime.SlimeAttack (player);

					//handles when the slime dies
					if (slime.Health <= 0) {
						player.GainArrow ();

						player.score += 50;

						slime.IsActive = false;

						asset.slimes.RemoveAt (i);  //removes the slime that was hit by projectile and gives play 'x' amount of arrows
					}
				}

                    //Arrows: can be ONLY when facing left or right
				if (player.arrowSupply > 0) {
					if (kbState.IsKeyDown (Keys.Space) && arrowExist == false) {
						int arrowX = player.position.X + 100;
						int arrowY = player.position.Y + 40;

						if (player.direction == Direction.RIGHT) {
							arrow = new Arrow (100, asset.arrowTexture, arrowX, arrowY);

							arrowExist = true;

							arrow.Collision (asset.slimes);

							player.timer = 0;

							player.UseArrow ();
						}

						if (player.direction == Direction.LEFT) {
							arrowX -= 200;

							arrow = new Arrow (100, asset.arrowTexture, arrowX, arrowY);

							arrowExist = true;

							arrow.Collision (asset.slimes);

							player.timer = 0;


							player.UseArrow ();                                
						}
					}


					if (player.timer > 60) {
						arrowExist = false;
						arrow.timer = 0;
					}
				}

				if (player.health <= 0) {
					game.state = GameState.GAMEOVER;
				}
				break;

			//OPTIONS case: will display the sound options, etc.
			case GameState.OPTIONS:

				bgm.PlayOptions ();



					//TODO
					/*Code for changing volume and putting it in the options screen
                        Process firstProc = new Process();
                        
                        Process firstProc = new Process();
                        firstProc.StartInfo.FileName = "oneWayOutExternalTool.exe";
                        firstProc.EnableRaisingEvents = true;

                        firstProc.Start();

                        firstProc.WaitForExit();
                        */
				break;

			//GAME OVER case: displays the highscores for the players and gives the options to go back to GAME or START
			case GameState.GAMEOVER:

				bgm.PlayGameOver ();

				break;

			//PAUSE case: stops all movement and music in-game
			case GameState.PAUSE:

				bgm.Pause ();

				break;
			}

			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			GraphicsDevice.Clear (Color.CornflowerBlue);

			// TODO: Add your drawing code here

			spriteBatch.Begin ();

			switch (game.state) {
			//Draw Menu
			case GameState.START:

				background.DrawStart (spriteBatch, GraphicsDevice);

				foregroundText.DrawStart (spriteBatch);

				break;

			//Draw Game
			case GameState.GAME:
				asset.DrawDungeon (spriteBatch);

				scoreChecked = false;

				spriteBatch.Draw (health, new Rectangle (4, 5, 102, 31), Color.Black);

				spriteBatch.Draw (health, healthSize, Color.White);

				highscoreText.DrawScore (spriteBatch, player);

				player.Draw (spriteBatch);

				if (arrowExist == true && player.arrowSupply > 0) {
					if (IsActive) {
						if (player.direction == Direction.RIGHT) {
							spriteBatch.Draw (asset.arrowTexture, arrow.position, Color.White);
						}
						if (player.direction == Direction.LEFT) {
							spriteBatch.Draw (asset.arrowTexture, arrow.position, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
						}
					}
				}

				asset.DrawSlimes (spriteBatch, foregroundText);

				foregroundText.DrawGame (spriteBatch, player);

				break;

			//Draw Help
			case GameState.HELP:

				background.DrawHelp (spriteBatch, GraphicsDevice);

				spriteBatch.Draw (signPicture, new Rectangle (300, 290, 200, 180), Color.White);

				foregroundText.DrawHelp (spriteBatch);

				break;

			//Draw Options
			case GameState.OPTIONS:

				background.DrawOption (spriteBatch, GraphicsDevice);

				break;

			//Draw Game Over
			case GameState.GAMEOVER:

				background.DrawGameover (spriteBatch, GraphicsDevice);

				foregroundText.DrawGameover (spriteBatch);

				highscoreText.readScore ();

				if (scoreChecked == false) {
					highscoreText.CheckScore (player.score);
					scoreChecked = true;
				}

				highscoreText.DrawScore (spriteBatch);
				break;

			//Draw Pause
			case GameState.PAUSE:

				asset.DrawDungeon (spriteBatch);

				spriteBatch.Draw (health, new Rectangle (4, 5, 102, 31), Color.Black);

				spriteBatch.Draw (health, healthSize, Color.White);

				player.Draw (spriteBatch);

				highscoreText.DrawScore (spriteBatch, player);

				asset.DrawSlimes (spriteBatch, foregroundText);

				foregroundText.DrawPause (spriteBatch);

				break;
			}

			spriteBatch.End ();
			base.Draw (gameTime);
		}

		//Returns a bool for a key press
		public bool SingleKeyPress (Keys keys)
		{
			return kbState.IsKeyDown (keys) && previousKbState.IsKeyUp (keys);
		}

		//Resets the game if player dies or quits
		public void ResetGame ()
		{
			player.health = 100;
			player.score = 0;
			player.arrowSupply = 5;
			player.SetPositionCenter (GraphicsDevice);
			//add new slime for the player            
		}

		//Draws new slime after clearing out all slime
		public void NextWave ()
		{
			//do what is done in the above method
		}
	}
}