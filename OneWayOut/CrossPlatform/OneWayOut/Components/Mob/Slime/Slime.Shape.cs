using System;

using System.Collections;

namespace OneWayOut.Components
{
    /// <summary>
    /// Slime shape values.
    /// </summary>
    partial class Slime
    {
        enum SlimeTextureMap
        {
            NULL,
            BODY,
            EYE
        }

        internal byte[][] movingU = new byte[][] {
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 0, 0, 1, 1, 1, 1, 0, 0 },
            new byte[] { 0, 1, 1, 2, 1, 2, 1, 0 },
            new byte[] { 0, 1, 1, 2, 1, 2, 1, 0 },
            new byte[] { 0, 1, 1, 1, 1, 1, 1, 0 },
            new byte[] { 0, 1, 0, 1, 0, 0, 1, 0 }
        };

        internal byte[][] movingR = new byte[][] {
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 0, 0, 1, 1, 1, 1, 0, 0 },
            new byte[] { 1, 1, 1, 2, 1, 2, 1, 0 },
            new byte[] { 0, 1, 1, 2, 1, 2, 1, 0 },
            new byte[] { 1, 1, 1, 1, 1, 1, 1, 0 },
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
        };


        internal byte[][] body = new byte[][] {
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 0, 0, 1, 1, 1, 1, 0, 0 },
            new byte[] { 0, 1, 1, 2, 1, 2, 1, 0 },
            new byte[] { 0, 1, 1, 2, 1, 2, 1, 0 },
            new byte[] { 0, 1, 1, 1, 1, 1, 1, 0 },
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        internal byte[][] blop = new byte[][] {
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 0, 1, 1, 1, 1, 1, 1, 0 },
            new byte[] { 1, 1, 1, 1, 1, 1, 1, 1 },
            new byte[] { 0, 1, 1, 1, 1, 1, 1, 0 },
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
        };

    }
}

