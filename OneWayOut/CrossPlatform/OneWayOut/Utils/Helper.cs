using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using OneWayOut.Components;

namespace OneWayOut.Utils
{
	class Helper:GameObject
	{
		public Helper (int x, int y, int width, int height, GraphicsDevice gp)
			: base (x, y, width, height)
		{

		}

		public static int MovingInterval (TimeSpan elapsed, int speed)
		{
			return (int)(elapsed.TotalMilliseconds / 1000 * speed);
		}


		void DrawLine (
			SpriteBatch spriteBatch, 
			Vector2 start,
			Vector2 end
		)
		{

			Vector2 edge = end - start;
			// calculate angle to rotate line
			float angle =
				(float)Math.Atan2 (edge.Y, edge.X);


			spriteBatch.Draw (texture,
				new Rectangle (// rectangle defines shape of line and position of start of line
					(int)start.X,
					(int)start.Y,
					(int)edge.Length (), //sb will strech the texture to fill this rectangle
					1), //width of line, change this to make thicker line
				null,
				Color.Red, //colour of line
				angle,     //angle of line (calulated above)
				new Vector2 (0, 0), // point in line about which to rotate
				SpriteEffects.None,
				0);
		}
	}
}

