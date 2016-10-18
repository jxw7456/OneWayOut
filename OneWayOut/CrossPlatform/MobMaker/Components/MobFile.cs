using System;
using Newtonsoft.Json;
using System.IO;

namespace MobMaker
{
	public class MobFile
	{
		const byte VERSION = 1;

		public MobFile ()
		{
			
		}

		public static void Write (string fileName, byte[][] shape)
		{
			using (var binaryWriter = new BinaryWriter (File.OpenWrite (fileName))) {
				
				binaryWriter.Write (VERSION);

				binaryWriter.Write (shape.Length);

				binaryWriter.Write (shape [0].Length);

				for (int i = 0; i < shape.Length; ++i) {

					var row = shape [i];

					for (int j = 0; j < row.Length; ++j) {

						binaryWriter.Write (row [j]);
					}
				}
			}
		}
	}
}

