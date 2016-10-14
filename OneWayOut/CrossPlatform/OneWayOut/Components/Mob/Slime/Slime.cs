using System;

using Microsoft.Xna.Framework.Graphics;

using OneWayOut.Components;
using Microsoft.Xna.Framework;
using OneWayOut.Utils;
using System.Threading;

namespace OneWayOut.Components.Slime
{
	partial class Slime: GameObject
	{
		const int PIXEL_SIZE = 9;

		const int MAX_MOVE_SPEED = 500;

		const int MIN_MOVE_SPEED = 200;

		SlimeState state;

		SlimeDirection direction;

		Random random;

		string name;

		int speed;

		Color color;

		//Time elapsed since the last check
		float elapsedTime = 0;

		float slimeDelay;

		public Slime (int x, int y, int width, int height, GraphicsDevice gp, Random r, string n)
			: base (x, y, width, height)
		{
			state = SlimeState.IDLE;

			direction = SlimeDirection.RIGHT;

			random = r;

			speed = random.Next (MIN_MOVE_SPEED, MAX_MOVE_SPEED);

			name = n;

			//name += speed;

			slimeDelay = (float)random.NextDouble () / 2;
			
			color = ColorGenerator.RandomColor (r);
		}
	}
}

