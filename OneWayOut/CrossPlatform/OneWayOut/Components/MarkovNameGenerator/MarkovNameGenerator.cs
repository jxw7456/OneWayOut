using System;

using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace OneWayOut.Components
{
	public partial class MarkovNameGenerator
	{
		const string SAMPLE_FILE = @"./SampleName.txt";

		const int SAMPLE_ORDER = 3;

		const int SAMPLE_LENGTH = 3;


		#region fields

		private Dictionary<string, List<char>> _chains = new Dictionary<string, List<char>> ();
		private List<string> _samples = new List<string> ();
		private List<string> _used = new List<string> ();
		private Random _rnd = new Random ();
		private int _order;
		private int _minLength;

		#endregion

		public MarkovNameGenerator ()
		{
			List<string> names = new List<string> ();
			
			using (var reader = new StreamReader (SAMPLE_FILE)) {
				string line;
				while ((line = reader.ReadLine ()) != null) {
					names.Add (line);
				}
				Warmup (names, SAMPLE_ORDER, SAMPLE_LENGTH);
			}
		}

		//constructor
		public void Warmup (IEnumerable<string> sampleNames, int order, int minLength)
		{
			

			//fix parameter values
			if (order < 1)
				order = 1;
			if (minLength < 1)
				minLength = 1;

			_order = order;
			_minLength = minLength;

			//split comma delimited lines
			foreach (string line in sampleNames) {
				string[] tokens = line.Split (',');
				foreach (string word in tokens) {
					string upper = word.Trim ().ToUpper ();
					if (upper.Length < order + 1)
						continue;                   
					_samples.Add (upper);
				}
			}

			//Build chains            
			foreach (string word in _samples) {              
				for (int letter = 0; letter < word.Length - order; letter++) {
					string token = word.Substring (letter, order);
					List<char> entry = null;
					if (_chains.ContainsKey (token))
						entry = _chains [token];
					else {
						entry = new List<char> ();
						_chains [token] = entry;
					}
					entry.Add (word [letter + order]);
				}
			}
		}

		//Get a random letter from the chain
		private char GetLetter (string token)
		{
			if (!_chains.ContainsKey (token))
				return '?';
			List<char> letters = _chains [token];
			int n = _rnd.Next (letters.Count);
			return letters [n];
		}

	}
}


