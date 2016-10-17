﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using OneWayOut.Components.Slime;

namespace OneWayOut
{
	/// <summary>
	/// Foreground text manager.
	/// In charge on rendering text for each scene
	/// </summary>
	class ForegroundTextManager
	{
		SpriteFont boldFont;

		SpriteFont biggerFont;

		SpriteFont owoFont;

		/// <summary>
		/// Initializes a new instance of the <see cref="OneWayOut.ForegroundTextManager"/> class.
		/// Cached all font objects
		/// </summary>
		/// <param name="Content">Content.</param>
		public ForegroundTextManager (ContentManager Content)
		{
			boldFont = Content.Load<SpriteFont> (@"fonts/bold");

			biggerFont = Content.Load<SpriteFont> (@"fonts/biggerFont");

			owoFont = Content.Load<SpriteFont> (@"fonts/owo");
		}

		/// <summary>
		/// Draws the name of the slime.
		/// </summary>
		/// <param name="sb">SpriteBatch.</param>
		/// <param name="s">Slime to Draw.</param>
		public void DrawSlimeName (SpriteBatch sb, Slime s)
		{
			s.DrawName (sb, owoFont);
		}

		/// <summary>
		/// Draws the gameover text.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void DrawGameover (SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString (biggerFont, "YOU ARE DEAD", new Vector2 (225, 10), Color.Red);
			spriteBatch.DrawString (boldFont, "Press 'G' to Restart", new Vector2 (270, 410), Color.White);
			spriteBatch.DrawString (boldFont, "Press 'Enter' for Main Menu", new Vector2 (225, 440), Color.White);
		}

		/// <summary>
		/// Draws the help text.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void DrawHelp (SpriteBatch spriteBatch)
		{
			//Story
			spriteBatch.DrawString (boldFont, "Story", new Vector2 (0, 0), Color.Red);
			spriteBatch.DrawString (boldFont, "You are an archer and you went on an expedition", new Vector2 (0, 20), Color.White);
			spriteBatch.DrawString (boldFont, "with a large army. After going down many floors,", new Vector2 (0, 40), Color.White);
			spriteBatch.DrawString (boldFont, "you found out that everyone is dead and you are", new Vector2 (0, 60), Color.White);
			spriteBatch.DrawString (boldFont, "the only one left. You must fight your way out!", new Vector2 (0, 80), Color.White);

			//Controls
			spriteBatch.DrawString (boldFont, "Controls", new Vector2 (0, 120), Color.Red);
			spriteBatch.DrawString (boldFont, "Up - Arrow Key Up", new Vector2 (0, 140), Color.White);
			spriteBatch.DrawString (boldFont, "Down - Arrow Key Down", new Vector2 (350, 140), Color.White);
			spriteBatch.DrawString (boldFont, "Left - Arrow Key Left", new Vector2 (0, 160), Color.White);
			spriteBatch.DrawString (boldFont, "Right - Arrow Key Right", new Vector2 (350, 160), Color.White);
			spriteBatch.DrawString (boldFont, "Shoot - Each Monster has a given", new Vector2 (0, 200), Color.White);
			spriteBatch.DrawString (boldFont, "sign language", new Vector2 (520, 200), Color.Red);
			spriteBatch.DrawString (boldFont, "above them. Enter the cooresponding letter on", new Vector2 (0, 220), Color.White);
			spriteBatch.DrawString (boldFont, "your keyboard to shoot your arrows.", new Vector2 (0, 240), Color.White);
			spriteBatch.DrawString (boldFont, "Refer to the image given below for help: ", new Vector2 (0, 260), Color.OrangeRed);

		}

		/// <summary>
		/// Draws the option text.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void DrawOption (SpriteBatch spriteBatch)
		{
			//external tool not drawn on screen yet
		}

		/// <summary>
		/// Draws the start text.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void DrawStart (SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString (biggerFont, "One Way Out", new Vector2 (225, 10), Color.White);
			spriteBatch.DrawString (boldFont, "Press 'Enter' to Start", new Vector2 (235, 180), Color.OrangeRed);
			spriteBatch.DrawString (boldFont, "Press 'H' for Help", new Vector2 (270, 210), Color.OrangeRed);
			spriteBatch.DrawString (boldFont, "Press 'O' for Options", new Vector2 (240, 240), Color.OrangeRed);                    
			spriteBatch.DrawString (boldFont, "Press 'Esc' to Quit", new Vector2 (255, 440), Color.Red);
		}

		/// <summary>
		/// Draws the pause text.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void DrawPause (SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString (biggerFont, "PAUSED", new Vector2 (300, 200), Color.DarkOrange);
			spriteBatch.DrawString (boldFont, "Press 'Q' to Quit", new Vector2 (255, 440), Color.DarkRed);
		}
	}
}

