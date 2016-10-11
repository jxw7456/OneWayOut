using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace OneWayOut
{
	public class ForegroundTextManager
	{
		SpriteFont spriteFont1;

		SpriteFont spriteFont2;

		public ForegroundTextManager (ContentManager Content)
		{
			spriteFont1 = Content.Load<SpriteFont> (@"fonts/bold");

			spriteFont2 = Content.Load<SpriteFont> (@"fonts/biggerFont");
		}

		public void DrawGameover (SpriteBatch spriteBatch)
		{

		}

		public void DrawHelp (SpriteBatch spriteBatch)
		{
			//Story
			spriteBatch.DrawString (spriteFont1, "Story", new Vector2 (0, 0), Color.Red);
			spriteBatch.DrawString (spriteFont1, "You are an archer and you went on an expedition", new Vector2 (0, 20), Color.White);
			spriteBatch.DrawString (spriteFont1, "with a large army. After going down many floors,", new Vector2 (0, 40), Color.White);
			spriteBatch.DrawString (spriteFont1, "you found out that everyone is dead and you are", new Vector2 (0, 60), Color.White);
			spriteBatch.DrawString (spriteFont1, "the only one left. You must fight your way out!", new Vector2 (0, 80), Color.White);

			//Controls
			spriteBatch.DrawString (spriteFont1, "Controls", new Vector2 (0, 120), Color.Red);
			spriteBatch.DrawString (spriteFont1, "Up - Arrow Key Up", new Vector2 (0, 140), Color.White);
			spriteBatch.DrawString (spriteFont1, "Down - Arrow Key Down", new Vector2 (350, 140), Color.White);
			spriteBatch.DrawString (spriteFont1, "Left - Arrow Key Left", new Vector2 (0, 160), Color.White);
			spriteBatch.DrawString (spriteFont1, "Right - Arrow Key Right", new Vector2 (350, 160), Color.White);
			spriteBatch.DrawString (spriteFont1, "Shoot - Each Monster has a given", new Vector2 (0, 200), Color.White);
			spriteBatch.DrawString (spriteFont1, "sign language", new Vector2 (520, 200), Color.Red);
			spriteBatch.DrawString (spriteFont1, "above them. Enter the cooresponding letter on", new Vector2 (0, 220), Color.White);
			spriteBatch.DrawString (spriteFont1, "your keyboard to shoot your arrows.", new Vector2 (0, 240), Color.White);
			spriteBatch.DrawString (spriteFont1, "Refer to the image given below for help: ", new Vector2 (0, 260), Color.OrangeRed);

		}

		public void DrawOption (SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString (spriteFont2, "YOU ARE DEAD", new Vector2 (225, 10), Color.Red);
			spriteBatch.DrawString (spriteFont1, "Press 'G' to Restart", new Vector2 (270, 410), Color.White);
			spriteBatch.DrawString (spriteFont1, "Press 'Enter' for Main Menu", new Vector2 (225, 440), Color.White);                    

		}

		public void DrawStart (SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString (spriteFont2, "One Way Out", new Vector2 (225, 10), Color.White);
			spriteBatch.DrawString (spriteFont1, "Press 'Enter' to Start", new Vector2 (235, 180), Color.OrangeRed);
			spriteBatch.DrawString (spriteFont1, "Press 'H' for Help", new Vector2 (270, 210), Color.OrangeRed);
			spriteBatch.DrawString (spriteFont1, "Press 'O' for Options", new Vector2 (240, 240), Color.OrangeRed);                    
			spriteBatch.DrawString (spriteFont1, "Press 'Esc' to Quit", new Vector2 (255, 440), Color.Red);
		}
	}
}

