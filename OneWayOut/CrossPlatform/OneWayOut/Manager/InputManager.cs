using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWayOut.Manager
{
    class InputManager
    {
        const double TIMER = 2008.0;

        double timer;

        string typingStack;

        public string TypingStack { get { return typingStack; } }

        KeyboardState kbState, previousKbState;

        public InputManager()
        {
            ClearStack();
        }

        public bool PushShoot()
        {
            return SingleKeyPress(Keys.Space);
        }

        /// <summary>
        /// // Loop through all the enum to check for clicked state instead of going through each individually
        /// </summary>
        /// <param name="game"></param>
        /// <param name="bgm"></param>
        public void SwitchScene(GameManager game, BgmManager bgm)
        {
            switch (game.state)
            {
                //START case
                case GameState.START:
                    if (SingleKeyPress((Keys)GameState.HELP))
                    {
                        game.state = GameState.HELP;
                    }

                    if (SingleKeyPress(Keys.Enter))
                    {
                        game.state = GameState.STORY;
                    }

                    if (SingleKeyPress((Keys)GameState.CREDITS))
                    {
                        game.state = GameState.CREDITS;
                    }

                    break;

                //GAME case    
                case GameState.GAME:
                    bgm.Resume();
                    if (SingleKeyPress((Keys)GameState.PAUSE))
                    {
                        game.state = GameState.PAUSE;
                    }
                    break;

                //STORY case
                case GameState.STORY:
                    if (SingleKeyPress(Keys.Enter))
                    {
                        game.state = GameState.GAME;
                    }
                    break;

                //HELP case
                case GameState.HELP:
                    if (SingleKeyPress((Keys)GameState.START))
                    {
                        game.state = GameState.START;
                    }
                    break;

                //PAUSE case
                case GameState.PAUSE:
                    if (SingleKeyPress((Keys)GameState.GAME))
                    {
                        game.state = GameState.GAME;
                    }

                    if (SingleKeyPress((Keys)GameState.START))
                    {
                        game.state = GameState.START;
                    }
                    break;

                //GAMEOVER case
                case GameState.GAMEOVER:
                    if (SingleKeyPress(Keys.Enter))
                    {
                        game.state = GameState.GAME;
                    }

                    if (SingleKeyPress((Keys)GameState.START))
                    {
                        game.state = GameState.START;
                    }
                    break;

                //HELPER case
                //case GameState.HELPER:
                //does not switch between scenes
                //break;

                case GameState.CREDITS:

                    if (SingleKeyPress((Keys)GameState.START))
                    {
                        game.state = GameState.START;
                    }

                    break;
            }

            //Possible easier code to switch between scenes
            /*
            // Loop through all the enum to check for clicked state instead of going through each individually
            foreach (GameState state in Enum.GetValues(typeof(GameState)))
            {
                if (SingleKeyPress((Keys)state))
                {
                    if (state == GameState.GAME)
                    {
                        bgm.Resume();
                    }

                    Console.WriteLine(state);

                    game.state = state;
                }
            }
            */
        }

        public void ClearStack()
        {
            typingStack = "";
            timer = TIMER;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Record(GameTime gameTime)
        {
            /*
                Go through all the Keys
                If Keys is a character
                Then record it.
                If keys is Captchas or Shift, prepare it 
                to capture uppercase letter
             */

            if (typingStack.Length > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (timer < 0)
            {
                ClearStack();
            }

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (IsKeyAChar(key) && SingleKeyPress(key))
                {
                    typingStack += key;
                }

                if (typingStack.Length > 0 && key == Keys.Back && SingleKeyPress(key))
                {
                    typingStack = typingStack.Remove(typingStack.Length - 1);
                }
            }
        }

        /// <summary>
        /// Determines if a key is a character.
        /// </summary>
        /// <returns><c>true</c> if is key is char; otherwise, <c>false</c>.</returns>
        /// <param name="key">Key.</param>
        public static bool IsKeyAChar(Keys key)
        {
            return key >= Keys.A && key <= Keys.Z;
        }

        /// <summary>
        /// Store the keyboard state and cache its previous state
        /// </summary>
        public void CacheKeyboardState()
        {
            previousKbState = kbState;

            kbState = Keyboard.GetState();
        }

        /// <summary>
        /// Ensure a key is released before the next input event
        /// </summary>
        /// <param name="key">The key to be check</param>
        /// <returns>The state of the key, true if first pressed and false while it is still pressed</returns>
        public bool SingleKeyPress(Keys key)
        {
            return kbState.IsKeyDown(key) && previousKbState.IsKeyUp(key);
        }
    }
}
