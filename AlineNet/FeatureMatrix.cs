// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureMatrix.cs" company="">
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
//   The feature matrix containing the scores.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AlineNet
{
    /// <summary>
    /// The feature matrix containing the scores.
    /// </summary>
    internal class FeatureMatrix
    {
        /// <summary>
        /// Gets the matrix containing scores for each letter of the (latin) alphabet.
        /// </summary>
        private static readonly int[][] Matrix = 
            {
                new[]
                    {
                        Constants.Syl, Constants.Vel, Constants.Vwl, Constants.Vce, 0,
                        0, 0, 0, 0, Constants.Low, Constants.Cnt, 0, 0
                    }, // a
                new[]
                    {
                        Constants.Nsl, Constants.Blb, Constants.Stp, Constants.Vce, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // b
                new[]
                    {
                        Constants.Nsl, Constants.Alv, Constants.Afr, Constants.Vls, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // c
                new[]
                    {
                        Constants.Nsl, Constants.Alv, Constants.Stp, Constants.Vce, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // d
                new[]
                    {
                        Constants.Syl, Constants.Pal, Constants.Vwl, Constants.Vce, 0,
                        0, 0, 0, 0, Constants.Mid, Constants.Frt, 0, 0
                    }, // e
                new[]
                    {
                        Constants.Nsl, Constants.Lbd, Constants.Frc, Constants.Vls, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // f
                new[]
                    {
                        Constants.Nsl, Constants.Vel, Constants.Stp, Constants.Vce, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // g
                new[]
                    {
                        Constants.Nsl, Constants.Glo, Constants.Frc, Constants.Vls, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // h
                new[]
                    {
                        Constants.Syl, Constants.Pal, Constants.Vwl, Constants.Vce, 0,
                        0, 0, 0, 0, Constants.Hgh, Constants.Frt, 0, 0
                    }, // i
                new[]
                    {
                        Constants.Nsl, Constants.Alv, Constants.Afr, Constants.Vce, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // j
                new[]
                    {
                        Constants.Nsl, Constants.Vel, Constants.Stp, Constants.Vls, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // k
                new[]
                    {
                        Constants.Nsl, Constants.Alv, Constants.Apr, Constants.Vce, 0,
                        0, Constants.Lat, 0, 0, 0, 0, 0, 0
                    }, // l
                new[]
                    {
                        Constants.Nsl, Constants.Blb, Constants.Stp, Constants.Vce,
                        Constants.Nas, 0, 0, 0, 0, 0, 0, 0, 0
                    }, // m
                new[]
                    {
                        Constants.Nsl, Constants.Alv, Constants.Stp, Constants.Vce,
                        Constants.Nas, 0, 0, 0, 0, 0, 0, 0, 0
                    }, // n
                new[]
                    {
                        Constants.Syl, Constants.Vel, Constants.Vwl, Constants.Vce, 0,
                        0, 0, 0, 0, Constants.Mid, Constants.Bak, Constants.Rnd, 0
                    }, // o
                new[]
                    {
                        Constants.Nsl, Constants.Blb, Constants.Stp, Constants.Vls, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // p
                new[]
                    {
                        Constants.Nsl, Constants.Glo, Constants.Stp, Constants.Vls, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // q
                new[]
                    {
                        Constants.Nsl, Constants.Rfl, Constants.Apr, Constants.Vce, 0,
                        Constants.Ret, 0, 0, 0, 0, 0, 0, 0
                    }, // r
                new[]
                    {
                        Constants.Nsl, Constants.Alv, Constants.Frc, Constants.Vls, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // s
                new[]
                    {
                        Constants.Nsl, Constants.Alv, Constants.Stp, Constants.Vls, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // t
                new[]
                    {
                        Constants.Syl, Constants.Vel, Constants.Vwl, Constants.Vce, 0,
                        0, 0, 0, 0, Constants.Hgh, Constants.Bak, Constants.Rnd, 0
                    }, // u
                new[]
                    {
                        Constants.Nsl, Constants.Lbd, Constants.Frc, Constants.Vce, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // v
                new[]
                    {
                        Constants.Nsl, Constants.Vel, Constants.Vwl, Constants.Vce, 0,
                        0, 0, 0, 0, Constants.Hgh, Constants.Bak, Constants.Rnd,
                        Constants.Blb
                    }, // w
                new[]
                    {
                        Constants.Nsl, Constants.Vel, Constants.Frc, Constants.Vls, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    }, // x
                new[]
                    {
                        Constants.Nsl, Constants.Pal, Constants.Vwl, Constants.Vce, 0,
                        0, 0, 0, 0, Constants.Hgh, Constants.Frt, 0, 0
                    }, // y
                new[]
                    {
                        Constants.Nsl, Constants.Alv, Constants.Frc, Constants.Vce, 0,
                        0, 0, 0, 0, 0, 0, 0, 0
                    } // z
            };

        /// <summary>
        /// Indexer for the matrix
        /// </summary>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <returns>
        /// The <see cref="int[]"/>.
        /// </returns>
        public int[] this[int i] => Matrix[i];

        /// <summary>
        /// Gets the matrix.
        /// </summary>
        /// <returns>
        /// The <see cref="int[][]"/>.
        /// </returns>
        public static int[][] Get()
        {
            return Matrix;
        }
    }
}
