using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using OneWayOut.Components;
using Microsoft.Xna.Framework.Input;

namespace OneWayOut.Manager
{
    /// <summary>
    /// Foreground text manager.
    /// In charge on rendering text for each scene
    /// </summary>
    class ForegroundTextManager
    {
        // TODO: Refactor position and cache them instead of making new vector.
        SpriteFont boldFont;

        SpriteFont biggerFont;

        SpriteFont owoFont;

        Texture2D arrowCountDisplay;

        Texture2D signPicture;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayOut.ForegroundTextManager"/> class.
        /// Cached all font objects
        /// </summary>
        /// <param name="Content">Content.</param>
        public ForegroundTextManager(ContentManager Content)
        {
            boldFont = Content.Load<SpriteFont>(@"fonts/bold");

            biggerFont = Content.Load<SpriteFont>(@"fonts/biggerFont");

            owoFont = Content.Load<SpriteFont>(@"fonts/owo");

            owoFont.Spacing = 5;

            arrowCountDisplay = Content.Load<Texture2D>(@"textures/arrow");

            signPicture = Content.Load<Texture2D>(@"textures/signLanguage");
        }

        /// <summary>
        /// Draws the name of the slime.
        /// </summary>
        /// <param name="sb">SpriteBatch.</param>
        /// <param name="s">Slime to Draw.</param>
        public void DrawSlimeName(SpriteBatch sb, Slime s)
        {
            s.DrawName(sb, owoFont);
        }

        /// <summary>
        /// Draws the gameover text.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public void DrawGameover(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(biggerFont, "YOU ARE DEAD", new Vector2(720, 10), Color.Red);
            spriteBatch.DrawString(boldFont, "Press '" + (Keys)GameState.GAME + "' to Restart", new Vector2(735, 905), Color.White);
            spriteBatch.DrawString(boldFont, "Press '" + (Keys)GameState.START + "' for Main Menu", new Vector2(700, 975), Color.White);
        }

        public void DrawGame(SpriteBatch spriteBatch, Player archer)
        {
            spriteBatch.Draw(arrowCountDisplay, new Rectangle(200, 0, 75, 55), Color.White);
            spriteBatch.DrawString(biggerFont, ": " + archer.ArrowCount, new Vector2(280, -2), Color.White);
            spriteBatch.DrawString(biggerFont, "Press '" + (Keys)GameState.PAUSE + "' To Pause", new Vector2(1370, 975), Color.Red);
        }

        /// <summary>
        /// Draws the help text.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public void DrawHelp(SpriteBatch spriteBatch)
        {
            //Controls
            spriteBatch.DrawString(biggerFont, "Controls", new Vector2(0, 0), Color.Red);
            spriteBatch.DrawString(biggerFont, "Up - Arrow Key Up", new Vector2(300, 70), Color.White);
            spriteBatch.DrawString(biggerFont, "Down - Arrow Key Down", new Vector2(1000, 70), Color.White);
            spriteBatch.DrawString(biggerFont, "Left - Arrow Key Left", new Vector2(300, 140), Color.White);
            spriteBatch.DrawString(biggerFont, "Right - Arrow Key Right", new Vector2(1000, 140), Color.White);
            spriteBatch.DrawString(biggerFont, "Shoot - Each Monster has a given", new Vector2(0, 250), Color.White);
            spriteBatch.DrawString(biggerFont, "sign language", new Vector2(920, 250), Color.Red);
            spriteBatch.DrawString(biggerFont, "above them. Enter", new Vector2(1310, 250), Color.White);
            spriteBatch.DrawString(biggerFont, "the cooresponding letter on your keyboard to shoot your arrows.", new Vector2(0, 320), Color.White);
            spriteBatch.DrawString(biggerFont, "Refer to the image given below for help: ", new Vector2(0, 390), Color.OrangeRed);

            //sign language image
            spriteBatch.Draw(signPicture, new Rectangle(700, 470, 510, 500), Color.White);

            spriteBatch.DrawString(biggerFont, "Press '" + (Keys)GameState.START + "' To Go Back", new Vector2(680, 975), Color.Red);
        }

        /// <summary>
        /// Draws the option text.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public void DrawStory(SpriteBatch spriteBatch)
        {
            //Story
            spriteBatch.DrawString(biggerFont, "Story", new Vector2(0, 0), Color.Red);
            spriteBatch.DrawString(biggerFont, "You are an archer and you went on an expedition", new Vector2(0, 70), Color.White);
            spriteBatch.DrawString(biggerFont, "with a large army. After going down many floors,", new Vector2(0, 140), Color.White);
            spriteBatch.DrawString(biggerFont, "you found out that everyone is dead and you are", new Vector2(0, 210), Color.White);
            spriteBatch.DrawString(biggerFont, "the only one left.", new Vector2(0, 280), Color.White);
            spriteBatch.DrawString(boldFont, "You have ONE WAY OUT!", new Vector2(600, 480), Color.DarkRed);

            //Continue to game
            spriteBatch.DrawString(biggerFont, "Press '" + (Keys)GameState.GAME + "' to Continue", new Vector2(720, 975), Color.White);
        }

        /// <summary>
        /// Draws the start text.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public void DrawStart(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(biggerFont, "One Way Out", new Vector2(780, 10), Color.White);
            spriteBatch.DrawString(biggerFont, "Press '" + (Keys)GameState.STORY + "' to Start", new Vector2(720, 330), Color.OrangeRed);
            spriteBatch.DrawString(biggerFont, "Press '" + (Keys)GameState.HELP + "' for Help", new Vector2(721, 400), Color.OrangeRed);                   
            spriteBatch.DrawString(biggerFont, "Press 'Esc' to Quit", new Vector2(710, 975), Color.Red);
        }

        /// <summary>
        /// Draws the pause text.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public void DrawPause(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(boldFont, "PAUSED", new Vector2(810, 520), Color.DarkOrange);
            spriteBatch.DrawString(biggerFont, "Press '" + (Keys)GameState.GAME + "' to Resume", new Vector2(700, 590), Color.Red);
            spriteBatch.DrawString(biggerFont, "Press '" + (Keys)GameState.HELPER + "' for Helper", new Vector2(710, 660), Color.Red);
            spriteBatch.DrawString(biggerFont, "Press '" + (Keys)GameState.START + "' to Quit", new Vector2(715, 730), Color.Red);
        }

        /// <summary>
        /// Debug Drawing to test the thing
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawDebug(SpriteBatch spriteBatch, string msg)
        {
            spriteBatch.DrawString(owoFont, msg, new Vector2(0, 450), Color.White);
        }

    }
}

