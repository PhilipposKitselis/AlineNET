// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Aligner.cs" company="">
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
//   Defines the Aligner type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AlineNet.Aligners
{
    /// <summary>
    /// The aligner.
    /// </summary>
    public abstract class Aligner : IAligner
    {
        /// <summary>
        /// The parameters.
        /// </summary>
        protected Parameters Parameters;

        /// <summary>
        /// The score matrix
        /// </summary>
        protected int[][] ScoreMatrix;

        /// <summary>
        /// score computed by the DP routine
        /// </summary>
        protected int DpScore;

        /// <summary>
        /// minimal acceptable score
        /// </summary>
        protected float AcceptedScore;

        /// <summary>
        /// trace i.e. unambiguous alignment
        /// </summary>
        internal Stack Trace = new Stack();

        /// <summary>
        /// alignment found by DP routine
        /// </summary>
        internal Stack Out = new Stack();

        /// <summary>
        /// // cost of individual operations
        /// </summary>
        internal Stack Cost = new Stack();

        /// <summary>
        /// for ONE_ONLY mode
        /// </summary>
        protected bool FallThru;

        /// <summary>
        /// Gets the largest value of an integer array
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        protected int Max(params int[] numbers)
        {
            var max = int.MinValue;
            foreach (var n in numbers)
            {
                if (n > max)
                {
                    max = n;
                }
            }

            return max;
        }

        /// <summary>
        /// Wrapper function for the score matrix
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        protected int Score(int i, int j)
        {
            return (i >= 0) && (j >= 0) ? this.ScoreMatrix[i][j] : Constants.NoScore;
        }

        /// <summary>
        /// Filters alignments that violate alternating-skips rule of Covington
        /// </summary>
        /// <param name="stack">The stack</param>
        /// <returns></returns>
        internal bool Allowed(Stack stack)
        {
            for (var i = 1; i < stack.Top; i++)
            {
                if (stack[0][i] == Constants.Nul && stack[1][i - 1] == Constants.Nul)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The align.
        /// </summary>
        /// <param name="wordA">
        /// The word a.
        /// </param>
        /// <param name="wordB">
        /// The word b.
        /// </param>
        /// <returns>
        /// The <see cref="AlignmentResult"/>.
        /// </returns>
        public virtual AlignmentResult Align(string wordA, string wordB)
        {
            return new AlignmentResult(0, new char[0][]);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Aligner"/> class.
        /// </summary>
        protected Aligner()
        {
            this.ScoreMatrix = new int[Constants.Maxl][];
            for (var i = 0; i < this.ScoreMatrix.Length; i++)
            {
                this.ScoreMatrix[i] = new int[Constants.Maxl];
            }
        }
    }
}
