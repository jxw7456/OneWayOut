using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneWayOut
{
    class Mob
    {
        public string name { get; set; }
        public int speed { get; set; }
        public Rectangle position  { get; set; }
        public int damage { get; set; }
    }
}
