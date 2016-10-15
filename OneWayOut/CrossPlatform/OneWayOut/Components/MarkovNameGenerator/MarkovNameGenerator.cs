using System;

using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace OneWayOut.Components
{
	/// <summary>
	/// Markov Model name generator.
	/// Generate name based on a sample. The algorithm
	/// works by generate letter based on pattern 
	/// from the samples.
	/// </summary>
	public partial class MarkovNameGenerator
	{
		const string SAMPLE_FILE = @"./SampleName.txt";

		const int SAMPLE_ORDER = 3;

		const int SAMPLE_LENGTH = 3;

		#region fields

		private Dictionary<string, List<char>> chains = new Dictionary<string, List<char>> ();

		private List<string> samples = new List<string> ();

		private List<string> used = new List<string> ();

		private Random random = new Random ();

		private int order;

		private int minLength;

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="OneWayOut.Components.MarkovNameGenerator"/> class.
		/// Read the samples and warmup the markov table
		/// </summary>
		public MarkovNameGenerator ()
		{
			using (var reader = new StreamReader (SAMPLE_FILE)) {
				string line;
				while ((line = reader.ReadLine ()) != null) {
					samples.Add (line);
				}
				Warmup (SAMPLE_ORDER, SAMPLE_LENGTH);
			}
		}

		/// <summary>
		/// Warmup the markov chain table, under the specified order 
		/// and a minimum length.
		/// </summary>
		/// <param name="sampleNames">Sample names.</param>
		/// <param name="o">Order.</param>
		/// <param name="m">Minimum length.</param>
		public void Warmup (int o, int m)
		{

			// Just in case this is used outside
			if (o < 1)
				o = 1;
			
			if (m < 1)
				m = 1;

			order = o;

			minLength = m;

			//Build chains            
			foreach (string name in samples) {
				
				for (int letter = 0; letter < name.Length - o; letter++) {

					// Token is a substring of word of order 'o'.
					// e.g: name: "alice", o: 3,
					// tokens are: ali, lic, ice
					string token = name.Substring (letter, o);

					List<char> entry = null;

					// If the token does not exist in the chains,
					// instantiate a list of char for it.
					// Otherwise grab the instance from the chains
					if (!chains.ContainsKey (token)) {
						entry = new List<char> ();
						chains [token] = entry;
					} else {
						entry = chains [token];
					}

					// Add the letter into the chain
					entry.Add (name [letter + o]);
				}
			}
		}
	}
}


