using System;

namespace OneWayOut.Components
{
	public partial class MarkovNameGenerator
	{

		//Get the next random name
		public string NextName {
			get {
				//get a random token somewhere in middle of sample word                
				string s = "";
				do {
					int n = _rnd.Next (_samples.Count);
					int nameLength = _samples [n].Length;
					s = _samples [n].Substring (_rnd.Next (0, _samples [n].Length - _order), _order);
					while (s.Length < nameLength) {
						string token = s.Substring (s.Length - _order, _order);
						char c = GetLetter (token);
						if (c != '?')
							s += GetLetter (token);
						else
							break;
					}

					if (s.Contains (" ")) {
						string[] tokens = s.Split (' ');
						s = "";
						for (int t = 0; t < tokens.Length; t++) {
							if (tokens [t] == "")
								continue;
							if (tokens [t].Length == 1)
								tokens [t] = tokens [t].ToUpper ();
							else
								tokens [t] = tokens [t].Substring (0, 1) + tokens [t].Substring (1).ToLower ();
							if (s != "")
								s += " ";
							s += tokens [t];
						}
					} else
						s = s.Substring (0, 1) + s.Substring (1).ToLower ();
				} while (_used.Contains (s) || s.Length < _minLength);
				_used.Add (s);
				return s;
			}
		}

		//Reset the used names
		public void Reset ()
		{
			_used.Clear ();
		}
	}
}

