using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneWayOut.Components.Player
{
	/// <summary>
	/// This is a maybe-sample class to control the animation of the player
	/// </summary>
	partial class Player
	{
		//A single sprite's width and height
		const int PLAYER_TEXTURE_SIZE = 512;

		public Texture2D Texture { get; set; }

		private int currentFrame;

		public int blink = 0;

		public int row;

		public int column;

		//slow down animation
		private float timer = 0;

		private int millisecondsPerFrame = 100;

		public Player (Texture2D texture, int row, int column)
		{
			Texture = texture;
		}

		public void Update (GameTime gameTime)
		{
			timer += (float)gameTime.ElapsedGameTime.Milliseconds;
			if (timer > millisecondsPerFrame) {
				//increment current frame
				currentFrame++;                
				if (currentFrame > 2) {
					currentFrame = 0;
				}
				blink = (blink + 1) % 10;
				timer = 0;
			}
		}

		public void Draw (SpriteBatch spriteBatch, Vector2 location)
		{
			Rectangle sourceRectangle;

			row = 0;
			column = 0;


			switch (direction) {
			case Direction.UP:
				row = 2;
				column = 2;
				if (currentFrame == 1) {
					column = 1;
				}
				break;
			case Direction.DOWN:
				row = 0;
				column = 0;
				if (currentFrame == 1) {
					column = 1;
				}
				break;
			case Direction.LEFT:
			case Direction.RIGHT:
				row = 1;
				column = 1;
				if (currentFrame == 1) {
					column = 2;
				}
				break;
			case Direction.IDLE:
			default:
				if (blink == 0) {
					column = 2;
				}
				break;
			}

			sourceRectangle = new Rectangle (column * PLAYER_TEXTURE_SIZE, row * PLAYER_TEXTURE_SIZE, PLAYER_TEXTURE_SIZE, PLAYER_TEXTURE_SIZE);
			if (direction == Direction.LEFT) {
				spriteBatch.Draw (Texture, archerlocal, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
			} else {
				spriteBatch.Draw (Texture, archerlocal, sourceRectangle, Color.White);
			}
		}
	}
}
