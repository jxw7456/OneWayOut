﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Manager;

namespace MobMaker
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		//const float FPS = 30.0f;

		GraphicsDeviceManager graphics;

		SpriteBatch spriteBatch;

		CanvasManager canvas;

		MouseState oldState;

		SpriteFont spriteFont;

		SaveButton saveBtn;

		const string OUTPUT_FILE = "mob.owo";

		string debugMsg = "";

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			graphics.PreferredBackBufferWidth = 450;
			graphics.PreferredBackBufferHeight = 450;

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
            
			base.Initialize ();

			this.IsMouseVisible = true;

			// base.TargetElapsedTime = TimeSpan.FromSeconds (1.0f / FPS);
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			canvas = new CanvasManager (Content, GraphicsDevice);

			spriteFont = Content.Load<SpriteFont> (@"fonts/main");

			saveBtn = new SaveButton (GraphicsDevice);
			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
			#endif
            
			// TODO: Add your update logic here
            
			MouseState mouseState = Mouse.GetState ();

			if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released) {
				// do something here
				int x = mouseState.X;

				int y = mouseState.Y;

				if (saveBtn.Clicked (mouseState.Position)) {
					canvas.Save (OUTPUT_FILE);
					debugMsg = "SAVED!";
				} else {
					debugMsg = "";
					canvas.ToggleTile (x, y);
				}
			}

			oldState = mouseState; // this reassigns the old state so that it is ready for next time

			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.TransparentBlack);
            
			//TODO: Add your drawing code here
            
			spriteBatch.Begin ();

			canvas.Draw (spriteBatch);

			saveBtn.Draw (spriteBatch, spriteFont);

			spriteBatch.DrawString (
				spriteFont, 
				debugMsg, 
				new Vector2 (GraphicsDevice.Viewport.Bounds.Center.X - 50, 10),
				Color.White
			);	

			spriteBatch.End ();

			base.Draw (gameTime);
		}
	}
}
