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
        public void WalkLeft(TimeSpan elapsed)
        {
            Position.X -= Helper.MovingInterval(elapsed, Speed);
            direction = SlimeDirection.LEFT;
            state = SlimeState.WALK;
        }

        /// <summary>
        /// Walk Right.
        /// </summary>
        /// <param name="elapsed">Elapsed.</param>
        public void WalkRight(TimeSpan elapsed)
        {
            Position.X += Helper.MovingInterval(elapsed, Speed);
            direction = SlimeDirection.RIGHT;
            state = SlimeState.WALK;
        }

        /// <summary>
        /// Walk up.
        /// </summary>
        /// <param name="elapsed">Elapsed.</param>
        public void WalkUp(TimeSpan elapsed)
        {
            Position.Y -= Helper.MovingInterval(elapsed, Speed);
            // Add the UP direction on top of direction 
            // so the slime can be either right/up or left/up
            direction |= SlimeDirection.UP;
            state = SlimeState.WALK;
        }

        /// <summary>
        /// Walk down
        /// </summary>
        /// <param name="elapsed">Elapsed.</param>
        public void WalkDown(TimeSpan elapsed)
        {
            Position.Y += Helper.MovingInterval(elapsed, Speed);
            // Add the DOWN direction on top of direction
            // so the slime can be either right/down or left/down
            direction |= SlimeDirection.DOWN;
            state = SlimeState.WALK;
        }
    }
}