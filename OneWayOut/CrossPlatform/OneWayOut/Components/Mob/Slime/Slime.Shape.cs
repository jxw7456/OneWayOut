using System;

using System.Collections;
using System.Collections.Generic;

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
            EYE,
            COLORFUL
        }

        internal static byte[][] movingU = new byte[][]
        {
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 0, 0, 1, 1, 1, 1, 0, 0 },
            new byte[] { 0, 1, 1, 2, 1, 2, 1, 0 },
            new byte[] { 0, 1, 1, 2, 1, 2, 1, 0 },
            new byte[] { 0, 1, 1, 1, 1, 1, 1, 0 },
            new byte[] { 0, 1, 0, 1, 0, 0, 1, 0 }
        };

        internal static byte[][] movingR = new byte[][]
        {
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 0, 0, 1, 1, 1, 1, 0, 0 },
            new byte[] { 1, 1, 1, 2, 1, 2, 1, 0 },
            new byte[] { 0, 1, 1, 2, 1, 2, 1, 0 },
            new byte[] { 1, 1, 1, 1, 1, 1, 1, 0 },
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
        };


        internal static byte[][] idle = new byte[][]
        {
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 0, 0, 1, 1, 1, 1, 0, 0 },
            new byte[] { 0, 1, 1, 2, 1, 2, 1, 0 },
            new byte[] { 0, 1, 1, 2, 1, 2, 1, 0 },
            new byte[] { 0, 1, 1, 1, 1, 1, 1, 0 },
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        internal static byte[][] blop = new byte[][]
        {
            new byte[] { 1, 0, 0, 0, 0, 0, 0, 1 },
            new byte[] { 0, 1, 0, 0, 0, 0, 1, 0 },
            new byte[] { 0, 1, 1, 1, 1, 1, 1, 0 },
            new byte[] { 0, 1, 1, 1, 1, 1, 1, 1 },
            new byte[] { 0, 1, 0, 0, 0, 0, 1, 0 },
            new byte[] { 1, 0, 0, 0, 0, 0, 0, 1 }
        };

        internal static Dictionary<string, byte[][]> Shapes = new Dictionary<string, byte[][]>()
        {
            { "IDLE", idle },
            { "BLOP", blop },
            { "UP", movingU },
            { "RIGHT", movingR }
        };
    }
}

