// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Word.cs" company="">
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
//   The word.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AlineNet
{
    /// <summary>
    /// The word.
    /// </summary>
    internal partial class Word
    {
        #region private
        /// <summary>
        /// Feature matrix assignments for individual segments
        /// </summary>
        private static readonly int[][] IndividualMatrix = AlineNet.FeatureMatrix.Get();

        /// <summary>
        /// The word
        /// </summary>
        private readonly string word = string.Empty;

        /// <summary>
        /// The out variable in the original code
        /// </summary>
        private readonly char[] wordArrayCopy = new char[Constants.Elen];

        /// <summary>
        /// The Feature Matrix
        /// </summary>
        private readonly int[][] featureMatrix = new int[Constants.FtLen][];

        /// <summary>
        /// pointers to external representation
        /// </summary>
        private readonly int[] ind = new int[Constants.Plen];

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Word"/> class. 
        /// The default constructor
        /// </summary>
        /// <param name="word">
        /// The string representation of a word
        /// </param>
        public Word(string word)
        {
            Validate(word);
            this.word = word;
            this.wordArrayCopy = word.ToCharArray();
            this.CreateFeatureMatrix();
            ApplyRedundancyRules();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Word"/> class from being created. 
        /// </summary>
        private Word()
        {
        }

        /// <summary>
        /// The length of the string representation of the word
        /// outlen in the original code
        /// </summary>
        // ReSharper disable once StyleCop.SA1650
        public int Length => this.word.Length;

        /// <summary>
        /// Gets or sets the internal (phonemic) length
        /// </summary>
        internal int PhoneticLength { get; set; }

        /// <summary>
        /// The feature matrix.
        /// </summary>
        internal int[][] FeatureMatrix => this.featureMatrix;
    }
}
