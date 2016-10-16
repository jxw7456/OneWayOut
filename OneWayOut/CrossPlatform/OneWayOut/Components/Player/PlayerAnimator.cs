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
				spriteBatch.Draw (texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
			} else {
				spriteBatch.Draw (texture, position, sourceRectangle, Color.White);
			}
		}
	}
}
