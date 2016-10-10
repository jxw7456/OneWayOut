using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWayOut.Utils
{
    class ColorGenerator
    {
        const int COLOR_SEED = 9999;

        static public Color RandomColor(Random random)
        {
            return new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
        }

    }
}
