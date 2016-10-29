using System;
using System.Threading;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using OneWayOut.Components;
using OneWayOut.Utils;

namespace OneWayOut.Components
{
    /// <summary>
    /// Slime main class, contain constructors and fields.
    /// </summary>
    partial class Slime : GameObject
    {
        public string Name { get; set; }

        public int Speed { get; set; }

        public int Damage { get; set; }

        public int Health { get; set; }

        const int PIXEL_SIZE = 9;

        const int MAX_MOVE_SPEED = 500;

        const int MIN_MOVE_SPEED = 200;

        const string IDLE_SHAPE = "mob.owo";

        SlimeState state;

        SlimeDirection direction;

        Random random;

        Color color;

        //Time elapsed since the last check
        float elapsedTime = 0;

        float slimeDelay;

        string upperCaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayOut.Components.Slime.Slime"/> class.
        /// And assigning the properties value
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="gp">Graphic </param>
        /// <param name="r">Random object</param>
        /// <param name="n">Asigned name</param>
        public Slime(int x, int y, int width, int height, GraphicsDevice gp, Random r, string n)
            : base(x, y, width, height)
        {
            random = r;

            state = SlimeState.IDLE;

            direction = SlimeDirection.RIGHT;

            slimeDelay = (float)random.NextDouble() / 2;

            upperCaseName = n.ToUpper();

            Speed = random.Next(MIN_MOVE_SPEED, MAX_MOVE_SPEED);

            Name = n;

            //Name += Speed;
            Damage = 1;

            Health = 100;

            if (r.NextDouble() > 0.18)
            {
                color = ColorGenerator.RandomColor(r);
            }
            else
            {
                color = Color.Black;
            }

            var iShape = ReadBitMap(IDLE_SHAPE);

            if (iShape != null)
            {
                body = iShape;
            }
        }
    }
}

