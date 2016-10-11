using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using OneWayOut.Utils;

namespace OneWayOut.Components.Slime
{
	partial class Slime
	{
		
		enum SlimeState
		{
			WALK,
			BLOPPING,
			BLOPPED,
			IDLE
		}

		public void DrawPoint (
			SpriteBatch sb,
			Vector2 pos
		)
		{

			//int screenWidth = graphicDevice.Viewport.Width;

			//int screenHeight = graphicDevice.Viewport.Height;

			var point = new Rectangle (
				            (int)pos.X,
				            (int)pos.Y,
				            PIXEL_SIZE,
				            PIXEL_SIZE);


			// sb.Draw (texture, point, null, Color.White);
			sb.Draw (texture, point, null, ColorGenerator.RandomColor (random));

		}

		public void DrawShape (SpriteBatch sb, byte[][] shape)
		{

			for (int i = 0; i < shape.Length; ++i) {

				var row = shape [i];

				for (int j = 0; j < row.Length; ++j) {

					var point = (SlimeTextureMap)row [j];

					if (point.Equals (SlimeTextureMap.NULL)) {
						continue;
					}

					int x = position.X + j * PIXEL_SIZE;

					int y = position.Y + i * PIXEL_SIZE;

					if ((direction & SlimeDirection.LEFT) != 0) {
						x = position.X + (row.Length - j) * PIXEL_SIZE;
					} 

					if ((direction & SlimeDirection.DOWN) != 0) {
						y = position.Y + (shape.Length - i) * PIXEL_SIZE;
					}

					var pos = new Vector2 (x, y);

					switch (point) {
					case SlimeTextureMap.EYE:

						if (random.NextDouble () > 0.81) {
							DrawPoint (sb, pos);
						}
						break;
					case SlimeTextureMap.BODY:
					default:
						DrawPoint (sb, pos);
						break;
					}
				}
			}
		}


		public new void Draw (SpriteBatch sb)
		{

			byte[][] shape;

			switch (state) {
			case SlimeState.BLOPPED:
				shape = blop;
				break;
			case SlimeState.WALK:
				
				shape = movingR;

				if ((direction & SlimeDirection.UP) != 0 || (direction & SlimeDirection.DOWN) != 0) {
					shape = movingU;
				}

				break;
			case SlimeState.IDLE:
			default:
				shape = body;
				break;
			}
			
			DrawShape (sb, shape);
		}
	}
}

