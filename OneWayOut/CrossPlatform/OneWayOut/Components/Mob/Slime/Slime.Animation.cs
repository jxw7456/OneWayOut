using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using OneWayOut.Utils;

namespace OneWayOut.Components
{
	/// <summary>
	/// Slime Animation Helper.
	/// </summary>
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

		/// <summary>
		/// Walk left.
		/// </summary>
		/// <param name="elapsed">Elapsed.</param>
		public void WalkLeft (TimeSpan elapsed)
		{
			position.X -= Helper.MovingInterval (elapsed, speed);
			direction = SlimeDirection.LEFT;
			state = SlimeState.WALK;	
		}

		/// <summary>
		/// Walk Right.
		/// </summary>
		/// <param name="elapsed">Elapsed.</param>
		public void WalkRight (TimeSpan elapsed)
		{
			position.X += Helper.MovingInterval (elapsed, speed);
			direction = SlimeDirection.RIGHT;			
			state = SlimeState.WALK;
		}

		/// <summary>
		/// Walk up.
		/// </summary>
		/// <param name="elapsed">Elapsed.</param>
		public void WalkUp (TimeSpan elapsed)
		{
			position.Y -= Helper.MovingInterval (elapsed, speed);
			// Add the UP direction on top of direction 
			// so the slime can be either right/up or left/up
			direction |= SlimeDirection.UP; 
			state = SlimeState.WALK;
		}

		/// <summary>
		/// Walk down
		/// </summary>
		/// <param name="elapsed">Elapsed.</param>
		public void WalkDown (TimeSpan elapsed)
		{
			position.Y += Helper.MovingInterval (elapsed, speed);
			// Add the DOWN direction on top of direction
			// so the slime can be either right/down or left/down
			direction |= SlimeDirection.DOWN;
			state = SlimeState.WALK;
		}
	}
}