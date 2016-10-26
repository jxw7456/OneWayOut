using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using OneWayOut.Utils;

namespace OneWayOut.Components
{
    /// <summary>
    /// Slime Behavior Helper.
    /// </summary>
    partial class Slime
    {

        /// <summary>
        /// Chase the specified obj using gameTime to calculate speed.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="gameTime">Game time.</param>
        public void Chase(GameObject obj, GameTime gameTime)
        {
            ChaseZiggly(obj, gameTime);
        }

        /// <summary>
        /// Chases straight behavior.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="gameTime">Game time.</param>
        public void ChaseStraight(GameObject obj, GameTime gameTime)
        {
            var elapsed = gameTime.ElapsedGameTime;

            state = SlimeState.IDLE;

            if (position.X > obj.position.X)
            {
                WalkLeft(elapsed);
            }

            if (position.X < obj.position.X)
            {
                WalkRight(elapsed);
            }

            if (position.Y > obj.position.Y)
            {
                WalkUp(elapsed);
            }

            if (position.Y < obj.position.Y)
            {
                WalkDown(elapsed);
            }
        }

        /// <summary>
        /// Chases uniformly.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="gameTime">Game time.</param>
        public void ChaseUniformly(GameObject obj, GameTime gameTime)
        {
            var elapsed = gameTime.ElapsedGameTime;

            state = SlimeState.IDLE;

            if (position.X > obj.position.X)
            {
                WalkLeft(elapsed);
            }
            else if (position.X < obj.position.X)
            {
                WalkRight(elapsed);
            }
            else if (position.Y > obj.position.Y)
            {
                WalkUp(elapsed);
            }
            else if (position.Y < obj.position.Y)
            {
                WalkDown(elapsed);
            }
        }

        /// <summary>
        /// Chases ziggly.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="gameTime">Game time.</param>
        public void ChaseZiggly(GameObject obj, GameTime gameTime)
        {
            var elapsed = gameTime.ElapsedGameTime;

            elapsedTime += (float)elapsed.TotalSeconds;

            if (elapsedTime > slimeDelay)
            {

                elapsedTime -= slimeDelay;

                int i = random.Next(4);

                state = SlimeState.IDLE;

                switch (i)
                {
                    case 0:
                        if (position.X > obj.position.X)
                        {
                            WalkLeft(elapsed);
                        }
                        break;
                    case 1:
                        if (position.X < obj.position.X)
                        {
                            WalkRight(elapsed);
                        }
                        break;
                    case 2:
                        if (position.Y > obj.position.Y)
                        {
                            WalkUp(elapsed);
                        }
                        break;
                    case 3:
                        if (position.Y < obj.position.Y)
                        {
                            WalkDown(elapsed);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Manual control if needed
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public void ProcessInput(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            var elapsed = gameTime.ElapsedGameTime;

            state = SlimeState.IDLE;

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                WalkLeft(elapsed);
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                WalkRight(elapsed);
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                WalkUp(elapsed);
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                WalkDown(elapsed);
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                state = SlimeState.BLOPPED;
            }
        }
    }
}
