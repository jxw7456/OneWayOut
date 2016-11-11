using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWayOut.Components
{
    partial class Arrow : GameObject
    {
        /// <summary>
        ///Whether the arrow is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public new bool IsActive
        {
            get { return timer > 0; }
        }
    }
}
