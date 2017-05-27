// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlignmentResultExtensions.cs" company="">
// Author: Philip Kitselis
//
// This file is part of AlineNet.

// It includes ported C++ code originally developed by G.Kondrak (c) 2000
// and later modified by Sean Downey(2015) for the AlineR project. Much of the
// C++ code remains as is with minor modifications for the C# conversion.
//
// For more information on the algorithm of G.Kondrak visit:

// https://webdocs.cs.ualberta.ca/~kondrak/papers/thesis.pdf

// AlineNet is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// AlineNet is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with AlineNet.  If not, see<http://www.gnu.org/licenses/>.   
// </copyright>
// <summary>
//   The alignment result extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AlineNet.ExtensionMethods
{
    using System.Linq;
    using System.Text;
    using Aligners;

    /// <summary>
    /// The alignment result extensions.
    /// </summary>
    public static class AlignmentResultExtensions
    {
        /// <summary>
        /// Fills the gap between two words.
        /// Not used right now. Part of some experimental feature I've been trying
        /// </summary>
        /// <param name="alignment">The alignment result</param>
        /// <returns>An new hybrid word</returns>
        public static string[] Morph(this AlignmentResult alignment)
        {
            var string1 = new StringBuilder();
            var string2 = new StringBuilder();
            var skipChars = new char[] { ' ', '|' };
            var interchangable = new[]
            {
                new[] { 'p', 'b' },
                new[] { 'f', 'b' },
                new[] { 'f', 'p' },
                new[] { 'g', 'k' },
                new[] { 't', 'd' },
                new[] { 'm', 'n' },
                new[] { 'w', 'u' },
                new[] { 's', 'z' },
            };

            for (var i = 0; i < alignment[0].Length; i++)
            {
                if (skipChars.Contains(alignment[0][i]))
                {
                    continue;
                }

                string1.Append(alignment[0][i] == '-' ? alignment[1][i] : alignment[0][i]);
                string2.Append(alignment[1][i] == '-' ? alignment[0][i] : alignment[1][i]);

                foreach (var phonemes in interchangable)
                {
                    if (alignment[0][i] == phonemes[0] && alignment[1][i] == phonemes[1])
                    {
                        string1.Append(alignment[1][i]);
                    }

                    if (alignment[0][i] == phonemes[1] && alignment[1][i] == phonemes[0])
                    {
                        string1.Append(alignment[1][i]);
                    }
                }
            }

            return new string[] { string1.ToString(), string2.ToString() };
        }
    }
}
