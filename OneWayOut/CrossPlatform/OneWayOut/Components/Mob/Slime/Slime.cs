using System;

using Microsoft.Xna.Framework.Graphics;

using OneWayOut.Components;
using Microsoft.Xna.Framework;

namespace OneWayOut.Components.Slime
{
	partial class Slime: GameObject
	{
		const int PIXEL_SIZE = 9;
	
		const int MOVING_INTERVAL = 1;
		
		SlimeState state;

		SlimeDirection direction;

		Random random;

		public Slime (int x, int y, int width, int height, GraphicsDevice gp, Random r)
			: base (x, y, width, height)
		{
			state = SlimeState.IDLE;

			direction = SlimeDirection.RIGHT;

			random = r;
		}

		public void WalkToward (GameObject obj)
		{
			// IF position is around the obj, walk toward it
		}
	}
}

