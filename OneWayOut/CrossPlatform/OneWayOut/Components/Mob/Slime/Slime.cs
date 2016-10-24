using System;

using Microsoft.Xna.Framework.Graphics;

using OneWayOut.Components;
using Microsoft.Xna.Framework;
using OneWayOut.Utils;
using System.Threading;
using OneWayOut.Manager;

namespace OneWayOut.Components
{
    /// <summary>
    /// Slime main class, contain constructors and fields.
    /// </summary>
    partial class Slime : GameObject
    {
        const int PIXEL_SIZE = 9;

        const int MAX_MOVE_SPEED = 500;

        const int MIN_MOVE_SPEED = 200;

        const string IDLE_SHAPE = "mob.owo";

        SlimeState state;

        SlimeDirection direction;

        Random random;

        public string name;

        public int speed;

        private int damage;

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        private int health;

        Color color;

        //Time elapsed since the last check
        public float elapsedTime = 0;

        public float slimeDelay;

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
            state = SlimeState.IDLE;

            direction = SlimeDirection.RIGHT;

            random = r;

            speed = random.Next(MIN_MOVE_SPEED, MAX_MOVE_SPEED);

            name = n;

            damage = Damage;

            health = 100;

            //name += speed;

            slimeDelay = (float)random.NextDouble() / 2;

            color = ColorGenerator.RandomColor(r);

            var iShape = ReadBitMap(IDLE_SHAPE);

            if (iShape != null)
            {
                body = iShape;
            }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public void SlimeAttack(Player player)
        {
            Damage = 10;

            if (this.position.Intersects(player.position))
            {
                // Use property and kill the slime (ONE HIT from arrow KILLS)
                player.health -= Damage;
                if (player.health == 0)
                {
                    player.IsActive = false;                                 
                }
            }
        }
    }
}

