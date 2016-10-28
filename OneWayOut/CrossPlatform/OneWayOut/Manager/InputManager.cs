using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWayOut.Manager
{
    enum OWOKey
    {
        A = Keys.A
    }

    class InputManager
    {
        const double TIMER = 10.0;

        double timer;

        public string TypingStack { get; set; }

        KeyboardState kbState, previousKbState;

        public InputManager()
        {
            TypingStack = "";

            timer = TIMER;

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
            // Loop through all the enum to check for clicked state instead of going through each individually
            foreach (GameState state in Enum.GetValues(typeof(GameState)))
            {
                if (SingleKeyPress((Keys)state))
                {
                    Console.WriteLine(state);

                    game.state = state;

                    if (state == GameState.GAME)
                    {
                        bgm.Resume();
                    }
                }
            }
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
                If keys is Captchas or Shift, prepare some stuffs
             */
           

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
