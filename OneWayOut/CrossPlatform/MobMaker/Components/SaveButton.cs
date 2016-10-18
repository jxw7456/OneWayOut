using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MobMaker
{
	public class SaveButton
	{
		Rectangle pos;
		
		Texture2D buttonTexture;

		public SaveButton (GraphicsDevice Graphics)
		{
			buttonTexture = new Texture2D (Graphics, 1, 1);

			buttonTexture.SetData<Color> (new Color[] { Color.White });

			pos = new Rectangle (0, 0, 60, 30);
		}

		public bool Clicked (Point p)
		{
			return pos.Contains (p);
		}

		public void Draw (SpriteBatch sb, SpriteFont sf)
		{
			sb.Draw (buttonTexture, Vector2.Zero, pos, Color.White);
			
			sb.DrawString (
				sf, 
				"SAVE", 
				Vector2.UnitX * 2 + Vector2.UnitY * 5,
				Color.Black
			);	
		}
	}
}

