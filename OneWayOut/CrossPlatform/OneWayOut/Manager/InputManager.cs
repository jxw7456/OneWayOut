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

        public InputManager()
        {
            
        }

        public void Record(GameTime gameTime)
        {
            /*
                Go through all the Keys
                If Keys is a character
                Then record it.
                If keys is Captchas or Shift, prepare some stuffs
             */

        }



    }
}
