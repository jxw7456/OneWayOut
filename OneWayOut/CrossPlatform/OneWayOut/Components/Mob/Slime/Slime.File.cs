using System;

using Microsoft.Xna.Framework.Graphics;

using OneWayOut.Components;
using Microsoft.Xna.Framework;
using OneWayOut.Utils;
using System.Threading;
using System.IO;

namespace OneWayOut.Components.Slime
{
	/// <summary>
	/// Slime file class, processing owo mob shape files.
	/// </summary>
	partial class Slime: GameObject
	{
		public byte[][] ReadBitMap (string fileName)
		{
			try {
				using (var binaryReader = new BinaryReader (File.OpenRead (fileName))) {

					var w = (int)binaryReader.ReadByte ();
					var h = (int)binaryReader.ReadByte ();

					byte[][] shape = new byte[w] [];

					for (int i = 0; i < shape.Length; ++i) {

						shape [i] = new byte[h];

						var row = shape [i];

						for (int j = 0; j < row.Length; ++j) {
							row [j] = binaryReader.ReadByte ();
						}
					}

					return shape;	
				}	
			} catch (FileNotFoundException ex) {
				
				return null;
			}
		}
	}
}

