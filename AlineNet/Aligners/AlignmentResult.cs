// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlignmentResult.cs" company="">
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
//   The alignment result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AlineNet.Aligners
{
    using System.Linq;
    using AlineNet.ExtensionMethods;

    /// <summary>
    /// The alignment result.
    /// </summary>
    public class AlignmentResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlignmentResult"/> class.
        /// </summary>
        /// <param name="score">
        /// The score.
        /// </param>
        /// <param name="alignment">
        /// The alignment.
        /// </param>
        internal AlignmentResult(int score, char[][] alignment)
        {
            this.Score = score;
            this.Alignment = alignment.TrimTail();
            this.Efficiency = this.CalculateEfficiency();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AlignmentResult"/> class from being created.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private AlignmentResult()
        {
        }

        /// <summary>
        /// Gets the score.
        /// </summary>
        public int Score { get; internal set; }

        /// <summary>
        /// Gets the efficiency.
        /// </summary>
        public double Efficiency { get; }

        /// <summary>
        /// Gets the alignment.
        /// </summary>
        public char[][] Alignment { get; } = new char[2][];

        /// <summary>
        /// Indexer pointing to the alignment
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="char[]"/>.
        /// </returns>
        public char[] this[int index] => this.Alignment[index];

        /// <summary>
        /// The calculate efficiency.
        /// </summary>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        internal double CalculateEfficiency()
        {
            var wordLength = 0d;
            var outsideScope = 0;
            var missMatches = 0d;

            // for local alignments it is relevant to count within
            // the scope of the match
            var startCounting = false;

            for (var i = 0; i < this.Alignment[0].Length; i++)
            {
                if (this.Alignment[0][i] == '-' || this.Alignment[1][i] == '-')
                {

                    if(startCounting)
                    { 
                        missMatches++;
                    }
                }
                else if (this.Alignment[0][i] == '|' && this.Alignment[1][i] == '|')
                {
                    // once hitting the first | the counting can start
                    // this is important for local and semiglobal alignments
                    startCounting = !startCounting;
                    continue;
                }
                else if (this.Alignment[0][i] == 32 && this.Alignment[1][i] == 32)
                {
                    continue;
                }

                if (startCounting)
                {
                    wordLength++;
                }
                else if (wordLength > 0)
                {
                    // the string outside the scope should not be too big
                    // in order to avoid far fetched matches
                    // each character outside the scope gives a penalty of 5%
                    outsideScope++;
                }
                else if (wordLength == 0)
                {
                    outsideScope += 2;
                }
            }

            return ((wordLength - missMatches) / wordLength) - (outsideScope * 0.05);
        }
    }
}
