using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using OneWayOut.Utils;

namespace OneWayOut.Components.Slime
{
	partial class Slime
	{

		[Flags]
		enum SlimeDirection
		{
			LEFT = 0x1,
			RIGHT = 0x2,
			UP = 0x4,
			DOWN = 0x8,
		}

		public void WalkLeft (TimeSpan elapsed)
		{
			position.X -= Helper.MovingInterval (elapsed, speed);
			direction = SlimeDirection.LEFT;
			state = SlimeState.WALK;	
		}

		public void WalkRight (TimeSpan elapsed)
		{
			position.X += Helper.MovingInterval (elapsed, speed);
			direction = SlimeDirection.RIGHT;			
			state = SlimeState.WALK;
		}

		public void WalkUp (TimeSpan elapsed)
		{
			position.Y -= Helper.MovingInterval (elapsed, speed);
			direction |= SlimeDirection.UP;
			state = SlimeState.WALK;
		}

		public void WalkDown (TimeSpan elapsed)
		{
			position.Y += Helper.MovingInterval (elapsed, speed);
			direction |= SlimeDirection.DOWN;
			state = SlimeState.WALK;
		}
	}
}