// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sigma.cs" company="">
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
//   The sigma methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AlineNet
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using ExtensionMethods;

    /// <summary>
    /// The sigma methods.
    /// </summary>
    internal class Sigma
    {
        /// <summary>
        /// The sub.
        /// </summary>
        /// <param name="wordA">
        /// The word a.
        /// </param>
        /// <param name="posA">
        /// The pos a.
        /// </param>
        /// <param name="wordB">
        /// The word b.
        /// </param>
        /// <param name="posB">
        /// The pos b.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        // ReSharper disable once StyleCop.SA1650
        public static int Sub(Word wordA, int posA, Word wordB, int posB, Parameters parameters)
        {
            if (posA == 0 || posB == 0)
            {
                return Constants.NoScore;
            }

            var featuresA = wordA.FeatureMatrix[wordA.PhoneticLength - posA];
            var featuresB = wordB.FeatureMatrix[wordB.PhoneticLength - posB];

            var score = parameters.MaxScore - FeatureArrayDistance(featuresA, featuresB, parameters.SalienceCoefficients);

            if (featuresA.IsVowel())
            {
                score -= parameters.VowelHandicap;
            }

            if (featuresB.IsVowel())
            {
                score -= parameters.VowelHandicap;
            }

            return score;
        }

        /// <summary>
        /// Get score for insertion/deletion
        /// This is actually a useless method. 
        /// </summary>
        /// <param name="skippen"></param>
        /// <returns>The skip value</returns>
        // ReSharper disable once StyleCop.SA1614
        public static int Skip(int skippen)
        {
            return skippen;
        }

        /// <summary>
        /// Returns 1/0 whether the two segments are identical
        /// </summary>
        /// <param name="wordA"></param>
        /// <param name="posA"></param>
        /// <param name="wordB"></param>
        /// <param name="posB"></param>
        /// <returns>1 or 0</returns>
        // ReSharper disable once StyleCop.SA1614
        // ReSharper disable once StyleCop.SA1614
        public static int AreIdentical(Word wordA, int posA, Word wordB, int posB)
        {
            var pA = wordA.FeatureMatrix[wordA.PhoneticLength - posA];
            var pB = wordB.FeatureMatrix[wordB.PhoneticLength - posB];

            for (int f = 0; f < Constants.FtLen; f++)
            {
                if (pA[f] != pB[f])
                {
                    return 0;
                }
            }

            return 1;
        }

        /// <summary>
        /// Compute score for expansion/compresion
        /// </summary>
        /// <param name="wordA"></param>
        /// <param name="posA"></param>
        /// <param name="wordB"></param>
        /// <param name="pos1B"></param>
        /// <param name="pos2B"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int Expand(
            Word wordA,
            int posA, 
            Word wordB,
            int pos1B,
            int pos2B, 
            Parameters parameters)
        {
            if (posA == 0 || pos2B == 0)
            {
                return Constants.NoScore;
            }

            if (pos1B == 0)
            {
                return Constants.NoScore;
            }

            var pA = wordA.FeatureMatrix[wordA.PhoneticLength - posA];
            var p1B = wordB.FeatureMatrix[wordB.PhoneticLength - pos1B];
            var p2B = wordB.FeatureMatrix[wordB.PhoneticLength - pos2B];

            var d1 = FeatureArrayDistance(p1B, pA, parameters.SalienceCoefficients);
            var d2 = FeatureArrayDistance(p2B, pA, parameters.SalienceCoefficients);

            if ((d1 == 0) || (d2 == 0))
            {
                return Constants.NoScore;
            }

            var score = parameters.MaxCompressionScore - (d1 + d2);

            if (p1B.IsVowel() || p2B.IsVowel())
            {
                score -= parameters.VowelHandicap;
            }

            if (pA.IsVowel())
            {
                score -= parameters.VowelHandicap;
            }

            return score;
        }

        /// <summary>
        /// Deals with double articulations
        /// </summary>
        /// <param name="featuresA"></param>
        /// <param name="featuresB"></param>
        /// <param name="vec"></param>
        /// <returns></returns>
        private static int DoubleArticulations(int[] featuresA, int[] featuresB, int[] vec)
        {
            int pd = Math.Abs(featuresA[Constants.FPlace] - featuresB[Constants.FPlace]);

            if (featuresA[Constants.FDouble] != 0)
            {
                if (pd > Math.Abs(featuresA[Constants.FDouble] - featuresB[Constants.FPlace]))
                {
                    pd = Math.Abs(featuresA[Constants.FDouble] - featuresB[Constants.FPlace]);
                }
            }

            if (featuresB[Constants.FDouble] != 0)
            {
                if (pd > Math.Abs(featuresA[Constants.FPlace] - featuresB[Constants.FDouble]))
                {
                    pd = Math.Abs(featuresA[Constants.FPlace] - featuresB[Constants.FDouble]);
                }
            }

            if (featuresA[Constants.FDouble] != 0 && featuresB[Constants.FDouble] != 0)
            {
                if (pd > Math.Abs(featuresA[Constants.FDouble] - featuresB[Constants.FDouble]))
                {
                    pd = Math.Abs(featuresA[Constants.FDouble] - featuresB[Constants.FDouble]);
                }
            }

            return pd * vec[Constants.FPlace];
        }

        /// <summary>
        /// Computes the distance between two feature arrays
        /// </summary>
        /// <param name="featuresA"></param>
        /// <param name="featuresB"></param>
        /// <param name="vec"></param>
        /// <returns></returns>
        private static int FeatureArrayDistance(int[] featuresA, int[] featuresB, int[] vec)
        {
            int dist = 0;
            int d;

            Func<int, int> diff = f => ((d = featuresA[f] - featuresB[f]) > 0 ? d : -d);

            if (featuresA.IsVowel() && featuresB.IsVowel())
            {
                dist += diff(Constants.FSyl) * vec[Constants.FSyl];
                
                // Consider(FNasal);
                dist += diff(Constants.FNasal) * vec[Constants.FNasal];
                
                // Consider(FRetro);
                dist += diff(Constants.FRetro) * vec[Constants.FRetro];
                
                // Consider(FHigh);
                dist += diff(Constants.FHigh) * vec[Constants.FHigh];
                
                // Consider(FBack);
                dist += diff(Constants.FBack) * vec[Constants.FBack];
                
                // Consider(FRound);
                dist += diff(Constants.FRound) * vec[Constants.FRound];
                
                // Consider(FLong);
                dist += diff(Constants.FLong) * vec[Constants.FLong];
            }
            else
            {
                // Consider(FSyl);
                dist += diff(Constants.FSyl) * vec[Constants.FSyl];
                
                // Consider(FStop);
                dist += diff(Constants.FStop) * vec[Constants.FStop];
                
                // Consider(FVoice);
                dist += diff(Constants.FVoice) * vec[Constants.FVoice];
                
                // Consider(FNasal);
                dist += diff(Constants.FNasal) * vec[Constants.FNasal];
                
                // Consider(FRetro);
                dist += diff(Constants.FRetro) * vec[Constants.FRetro];
                
                // Consider(FLat);
                dist += diff(Constants.FLat) * vec[Constants.FLat];
                
                // Consider(FAsp);
                dist += diff(Constants.FAsp) * vec[Constants.FAsp];
                dist += DoubleArticulations(featuresA, featuresB, vec);        // was: Consider(FPlace);
            }

            return dist;
        }
    }
}
