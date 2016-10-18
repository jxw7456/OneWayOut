using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneWayOut.Components.Drop
{
    partial class Drop : GameObject
    {
        public int healingAmount { get; set; }

        //Constructor 
        public Drop(Texture2D texure, int x, int y, int width, int height) : base(x, y, width, height)
        {

        }
    }
}
