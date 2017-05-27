// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parameters.cs" company="">
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
//   The parameters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AlineNet
{
    using Exceptions;

    /// <summary>
    /// The parameters.
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// The salience coefficients.
        /// </summary>
        private int[] salienceCoefficients = new[]
        {
            5, 40, 50, 10, 10, 10, 10, 5, 1, 5, 5, 5, 10
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameters"/> class.
        /// </summary>
        public Parameters()
        {
            this.ScoreMargin = 1.0f;
            this.MaxScore = 3500;
            this.MaxCompressionScore = 4500;
            this.VowelHandicap = 1000;
            this.SkipCost = -1000;
        }

        /// <summary>
        /// Gets or sets the margin of tolerance
        /// </summary>
        public float ScoreMargin { get; set; }

        /// <summary>
        /// Gets or sets the maximal substitution score
        /// </summary>
        public int MaxScore { get; set; }

        /// <summary>
        /// Gets or sets the maximal compression score
        /// </summary>
        public int MaxCompressionScore { get; set; }

        /// <summary>
        /// Gets or sets the penalty for insertion/deletion
        /// </summary>
        public int SkipCost { get; set; }

        /// <summary>
        ///  Gets or sets the vowel handicap
        /// </summary>
        public int VowelHandicap { get; set; }

        /// <summary>
        /// Gets or sets the salience coefficients
        /// </summary>
        public int[] SalienceCoefficients
        {
            get
            {
                return this.salienceCoefficients;
            }

            set
            {
                if (value != null && value.Length == 13)
                {
                    this.salienceCoefficients = value;
                }
                else
                {
                    throw new ViolationException("Invalid salience coeficients passed. An array of 13 elements is required.");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether squashing should be used.
        /// </summary>
        public bool Squashing { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether one only.
        /// </summary>
        public bool OneOnly { get; set; } = true;       
    }
}
