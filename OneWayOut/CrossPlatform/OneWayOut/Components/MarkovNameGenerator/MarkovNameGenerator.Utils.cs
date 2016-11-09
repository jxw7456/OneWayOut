using System;
using System.Collections.Generic;

namespace OneWayOut.Components
{
    /// <summary>
    /// Generate name
    /// </summary>
    public partial class MarkovNameGenerator
    {
        public string RandomBottomCase(string s, int hardness = 0)
        {
            if (hardness == 0)
            {
                return s.ToUpper();
            }

            int l = s.Length;

            int uppercaseAmount = l - random.Next(hardness > l ? l : hardness);

            var sArray = s.ToCharArray();

            string o = "";

            for (int i = 0; i < l; ++i)
            {
                if (i < uppercaseAmount)
                {
                    o += char.ToUpper(sArray[i]);
                }
                else
                {
                    o += sArray[i];
                }
            }
            return o;
        }

        //Get the next random name
        /// <summary>
        /// Generate the next random name.
        /// </summary>
        /// <value>A random name</value>
        public string NextName
        {
            get
            {
                string s = "";

                do
                {
                    // Get a model sample
                    int n;

                    // Pick a sample with appropriate order, ensuring the length is ok
                    do
                    {
                        n = random.Next(samples.Count);
                    }
                    while (samples[n].Length < order);

                    int nameLength = samples[n].Length;

                    int start = random.Next(0, samples[n].Length - order);

                    // Get a random token
                    s = samples[n].Substring(start, order);

                    // Fill the remaining length with letter matching the token pattern
                    while (s.Length < nameLength)
                    {

                        string token = s.Substring(s.Length - order, order);

                        char c = GetLetter(token);

                        if (c != '?')
                            s += GetLetter(token);
                        else
                            break;
                    }

                    // Fill the empty char with letter matching the token
                    if (s.Contains(" "))
                    {
                        string[] tokens = s.Split(' ');

                        s = "";

                        for (int t = 0; t < tokens.Length; t++)
                        {
                            if (tokens[t] == "")
                                continue;
                            if (tokens[t].Length == 1)
                                tokens[t] = tokens[t].ToUpper();
                            else
                                tokens[t] = tokens[t].Substring(0, 1) + tokens[t].Substring(1).ToLower();
                            if (s != "")
                                s += " ";
                            s += tokens[t];
                        }
                    }
                    else
                        s = s.Substring(0, 1) + s.Substring(1).ToLower();
                } while (used.Contains(s) || s.Length < minLength);

                used.Add(s);

                return s;
            }
        }

        /// <summary>
        /// Gets a random letter from the model
        /// with root from the token.
        /// </summary>
        /// <returns>The letter.</returns>
        /// <param name="token">Root Token.</param>
        private char GetLetter(string token)
        {
            if (!chains.ContainsKey(token))
                return '?';

            List<char> letters = chains[token];

            int n = random.Next(letters.Count);

            return letters[n];
        }

        /// <summary>
        /// Reset the used markov table.
        /// </summary>
        public void Reset()
        {
            used.Clear();
        }
    }
}

