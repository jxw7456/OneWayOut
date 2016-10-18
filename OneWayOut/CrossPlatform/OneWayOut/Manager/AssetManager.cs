using System;

using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Content;

using OneWayOut.Components.Dungeon;

using OneWayOut.Components.Slime;

using OneWayOut.Components;

namespace OneWayOut.Manager
{
    /// <summary>
    /// Asset manager 
    /// Manages outstanding collections and list that 
    /// does not require much helper method.
    /// </summary>
    class AssetManager
    {
        const int RANDOM_SEED = 999;

        const int SLIME_SIZE = 90;

        const int SLIME_COUNT = 9;

        Random random;

        Texture2D slimeTexture;

        MarkovNameGenerator nameGen;

        public List<Slime> slimes;

        public Dungeon dungeon;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayOut.Manager.AssetManager"/> class.
        /// Init the Slime List and Dungeon Tiles
        /// </summary>
        /// <param name="Content">Content.</param>
        /// <param name="Graphics">Graphics.</param>
        public AssetManager(ContentManager Content, GraphicsDevice Graphics)
        {
            random = new Random();

            dungeon = new Dungeon(Content);

            dungeon.GenerateTiles(random);

            nameGen = new MarkovNameGenerator();

            InitSlime(Graphics);

            slimes = new List<Slime>();

            for (int i = 0; i < SLIME_COUNT; i++)
            {
                AddNewSlime(Graphics);
            }
        }

        /// <summary>
        /// Draws all the slimes.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        /// <param name="foreGroundText">Fore ground text.</param>
        public void DrawSlimes(SpriteBatch spriteBatch, ForegroundTextManager foreGroundText)
        {
            foreach (var slime in slimes)
            {
                slime.Draw(spriteBatch);
                foreGroundText.DrawSlimeName(spriteBatch, slime);
            }
        }

        /// <summary>
        /// Draws the dungeon.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public void DrawDungeon(SpriteBatch spriteBatch)
        {
            dungeon.Draw(spriteBatch);
        }

        /// <summary>
        /// Inits the slime texture.
        /// </summary>
        /// <param name="Graphics">Graphics.</param>
        void InitSlime(GraphicsDevice Graphics)
        {
            slimeTexture = new Texture2D(Graphics, 1, 1);

            slimeTexture.SetData<Color>(new Color[] { Color.White });
        }

        /// <summary>
        /// Adds new slime into the slime list.
        /// </summary>
        /// <param name="Graphics">Graphics.</param>
        void AddNewSlime(GraphicsDevice Graphics)
        {
            slimes.Add(MakeNewSlime(Graphics));
        }

        /// <summary>
        /// Generate a new random slime.
        /// </summary>
        /// <returns>A new slime.</returns>
        /// <param name="Graphics">Graphics.</param>
        Slime MakeNewSlime(GraphicsDevice Graphics)
        {
            var vp = Graphics.Viewport;

            var s = new Slime(random.Next(vp.Width), random.Next(vp.Height), SLIME_SIZE, SLIME_SIZE, Graphics, random, nameGen.NextName);

            s.texture = slimeTexture;

            return s;
        }
    }
}
