﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using OneWayOut.Utils;

namespace OneWayOut.Components
{
    /// <summary>
    /// Slime Draw Helper.
    /// </summary>
    partial class Slime
    {
        enum SlimeState
        {
            WALK,
            BLOPPING,
            BLOPPED,
            IDLE
        }

        /// <summary>
        /// Helper to draw a point.
        /// </summary>
        /// <param name="sb">Spritebatch</param>
        /// <param name="pos">Position of point</param>
        public void DrawPoint(SpriteBatch sb, Vector2 pos, Color c)
        {
            var point = new Rectangle((int)pos.X, (int)pos.Y, PIXEL_SIZE, PIXEL_SIZE);

            if (!c.Equals(Color.TransparentBlack))
            {
                sb.Draw(Texture, point, null, c);
            }
            else
            {
                sb.Draw(Texture, point, null, ColorGenerator.RandomColor(random));
            }
        }

        /// <summary>
        /// Draws the shape provided.
        /// </summary>
        /// <param name="sb">Sb.</param>
        /// <param name="shape">Shape.</param>
        public void DrawShape(SpriteBatch sb, byte[][] shape)
        {
            for (int i = 0; i < shape.Length; ++i)
            {
                var row = shape[i];

                for (int j = 0; j < row.Length; ++j)
                {
                    var point = (SlimeTextureMap)row[j];

                    // Skip 0 bits
                    if (point.Equals(SlimeTextureMap.NULL))
                    {
                        continue;
                    }

                    int x = Position.X + j * PIXEL_SIZE;

                    int y = Position.Y + i * PIXEL_SIZE;

                    // Rotate across the X axis if look left
                    if ((direction & SlimeDirection.LEFT) != 0)
                    {
                        x = Position.X + (row.Length - j) * PIXEL_SIZE;
                    }

                    // Rotate across the Y axis if look down
                    if ((direction & SlimeDirection.DOWN) != 0)
                    {
                        y = Position.Y + (shape.Length - i) * PIXEL_SIZE;
                    }

                    var pos = new Vector2(x, y);

                    // Switch between type of texture
                    switch (point)
                    {
                        case SlimeTextureMap.EYE:

                            // Simulate a blinking effect...
                            if (random.NextDouble() > 0.81)
                            {
                                DrawPoint(sb, pos, Color.Black);
                            }
                            break;
                        case SlimeTextureMap.COLORFUL:
                            DrawPoint(sb, pos, Color.TransparentBlack);

                            break;
                        case SlimeTextureMap.BODY:
                        default:
                            DrawPoint(sb, pos, color);
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Draw the sprite.
        /// </summary>
        /// <param name="sb">Sb.</param>
        public new void Draw(SpriteBatch sb)
        {
            // TODO: Refactor the string shape into something more relevant. 
            // The current implementation might be very shit
            // In the future...

            byte[][] shape;
            // Switch the shape based on the slime state
            switch (state)
            {
                case SlimeState.BLOPPED:
                    shape = Shapes["BLOP"];
                    break;

                case SlimeState.WALK:

                    shape = Shapes["RIGHT"];

                    if ((direction & SlimeDirection.UP) != 0 || (direction & SlimeDirection.DOWN) != 0)
                    {
                        shape = Shapes["UP"];
                    }

                    break;

                case SlimeState.IDLE:
                default:

                    shape = Shapes["IDLE"];
                    break;
            }

            DrawShape(sb, shape);
        }

        /// <summary>
        /// Draws the name of the slime.
        /// </summary>
        /// <param name="sb">Sprite Batch</param>
        /// <param name="sf">Font</param>
        public void DrawName(SpriteBatch sb, SpriteFont sf)
        {
            // Get the position, top-left of the sprite, topped with the font line spacing.
            // y run from top to bottom thus the '-' sign
            var pos = new Vector2((float)Position.Left, (float)(Position.Top - sf.LineSpacing));
                        
            sb.DrawString(sf, Name, new Vector2((float)Position.Left - 2, (float)(Position.Top - (sf.LineSpacing - 4))), Color.Black); //Shadow
            sb.DrawString(sf, Name, pos, Color.White);
        }
    }
}

