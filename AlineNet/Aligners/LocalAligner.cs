// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalAligner.cs" company="">
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
//   Defines the LocalAligner type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AlineNet.Aligners
{
    /// <summary>
    /// The local aligner.
    /// </summary>
    internal class LocalAligner : Aligner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalAligner"/> class.
        /// </summary>
        public LocalAligner()
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalAligner"/> class.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public LocalAligner(Parameters parameters)
        {
            this.Parameters = parameters;
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
        public override AlignmentResult Align(string wordA, string wordB)
        {
            if (this.Parameters == null)
            {
                this.Parameters = new Parameters();
            }

            var maxScore = 0;

            if (wordA.Length >= wordB.Length)
            {
                // get the max possible score
                maxScore = this.Align(new Word(wordA), new Word(wordA)).Score;
            }

            if (wordA.Length < wordB.Length)
            {
                // get the max possible score
                maxScore = this.Align(new Word(wordB), new Word(wordB)).Score;
            }

            var result = this.Align(new Word(wordA), new Word(wordB));
            result.Score = (int)((double)result.Score / maxScore * 100);
            return result;
        }

        /// <summary>
        /// The alignment.
        /// </summary>
        /// <param name="wA">
        /// The w a.
        /// </param>
        /// <param name="wB">
        /// The w b.
        /// </param>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <param name="j">
        /// The j.
        /// </param>
        /// <param name="T">
        /// The t.
        /// </param>
        /// <param name="getscore">
        /// The getscore.
        /// </param>
        /// <param name="aback1">
        /// The aback 1.
        /// </param>
        /// <param name="aback2">
        /// The aback 2.
        /// </param>
        private void Alignment(Word wA, Word wB, int i, int j, int T, ref int getscore, char[] aback1, char[] aback2)
        {
            if (this.Parameters.OneOnly && this.FallThru)
            {
                return;
            }

            if (i != 0 && j != 0)
            { 
                int subSc = Sigma.Sub(wA, i, wB, j, this.Parameters);
                if (this.Score(i - 1, j - 1) + subSc + T >= this.AcceptedScore)
                {
                    this.Cost.Push(subSc);
                    this.Out.Push(i, j);
                    this.Trace.Push(i, j);
                    this.Alignment(wA, wB, i - 1, j - 1, T + subSc, ref getscore, aback1, aback2);
                    this.Trace.Pop();
                    this.Out.Pop();
                    this.Cost.Pop();
                }

                if (i == 0)
                {
                    goto display;
                }

                int insSc = Sigma.Skip(this.Parameters.SkipCost);
                if ((i == 0) || (this.Score(i, j - 1) + insSc + T >= this.AcceptedScore))
                {
                    this.Cost.Push(insSc);
                    this.Out.Push(Constants.Nul, j);
                    this.Alignment(wA, wB, i, j - 1, T + insSc, ref getscore, aback1, aback2);
                    this.Out.Pop();
                    this.Cost.Pop();
                }

                if (this.Parameters.Squashing)
                { 
                    int expSc = Sigma.Expand(wA, i, wB, j - 1, j, this.Parameters);
                    if (this.Score(i - 1, j - 2) + expSc + T >= this.AcceptedScore)
                    {
                        this.Cost.Push(expSc);
                        this.Cost.Push(Constants.Nul);
                        this.Out.Push(i, j);
                        this.Out.Push(Constants.Dub, j - 1);
                        this.Trace.Push(i, j);
                        this.Trace.Push(i, j - 1);
                        this.Alignment(wA, wB, i - 1, j - 2, T + expSc, ref getscore, aback1, aback2);
                        this.Trace.Pop(2);
                        this.Out.Pop(2);
                        this.Cost.Pop(2);
                    }
                }

                if (j == 0)
                {
                    goto display; // shortcut
                }

                int delSc = Sigma.Skip(this.Parameters.SkipCost);
                if ((j == 0) || (this.Score(i - 1, j) + delSc + T >= this.AcceptedScore))
                {
                    this.Cost.Push(delSc);
                    this.Out.Push(i);
                    this.Alignment(wA, wB, i - 1, j, T + delSc, ref getscore, aback1, aback2);
                    this.Out.Pop();
                    this.Cost.Pop();
                }

                if (this.Parameters.Squashing)
                {
                    int cmpSc = Sigma.Expand(wB, j, wA, i - 1, i, this.Parameters);
                    if (this.Score(i - 2, j - 1) + cmpSc + T >= this.AcceptedScore)
                    {
                        this.Cost.Push(cmpSc);
                        this.Cost.Push(Constants.Nul);
                        this.Out.Push(i, j);
                        this.Out.Push(i - 1, Constants.Dub);
                        this.Trace.Push(i, j);
                        this.Trace.Push(i - 1, j);
                        this.Alignment(wA, wB, i - 2, j - 1, T + cmpSc, ref getscore, aback1, aback2);
                        this.Trace.Pop(2);
                        this.Out.Pop(2);
                        this.Cost.Pop(2);
                    }
                }

                if (this.Score(i, j) != 0)
                {
                    return;
                }
            }

            display:
            if (!this.Allowed(this.Out) || this.FallThru)
            {
                return;
            }

            this.Out.Push(Constants.Lim, Constants.Lim);

            // padding the remaining string
            for (var i1 = i; i1 > 0; i1--)
            {
                this.Out.Push(i1);
            }

            for (var j1 = j; j1 > 0; j1--)
            {
                this.Out.Push(Constants.Nul, j1);
            }

            this.Out.Alignment(wA, 0, aback1);

            this.Out.Alignment(wB, 1, aback2);

            this.Out.Pop(i + j + 1);

            int score = T;
            getscore = score / 100;

            if (this.Parameters.OneOnly)
            {
                this.FallThru = true;
            }
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
        public AlignmentResult Align(Word wordA, Word wordB)
        {
            var aback1 = new char[50];
            var aback2 = new char[50];
            var score = 0;
            var lenA = (short)wordA.PhoneticLength;
            var lenB = (short)wordB.PhoneticLength;

            this.Cost.Clear();
            this.Trace.Clear();
            this.Out.Clear();
            this.FallThru = false;

            var sgmax = this.Similarity(wordA, wordB);
            this.DpScore = sgmax;

            this.AcceptedScore = this.DpScore * this.Parameters.ScoreMargin;

            for (var i = 0; i <= lenA; i++)
            {
                for (var j = 0; j <= lenB; j++)
                {
                    if (this.ScoreMatrix[i][j] >= this.AcceptedScore)
                    {
                        for (var j1 = lenB; j1 > j; j1--)
                        {
                            this.Out.Push(Constants.Nul, j1);
                        }

                        for (var i1 = lenA; i1 > i; i1--)
                        {
                            this.Out.Push(i1);
                        }

                        this.Out.Push(Constants.Lim, Constants.Lim);
                        this.Alignment(wordA, wordB, i, j, 0, ref score, aback1, aback2);
                        this.Out.Pop(lenA - i + lenB - j + 1);
                        if (this.FallThru)
                        {
                            return new AlignmentResult(
                                    score,
                                    new[] { aback1, aback2 });
                        }
                    }
                }
            }

            return new AlignmentResult(
                       score,
                       new[] { aback1, aback2 });
        }

        /// <summary>
        /// The similarity.
        /// </summary>
        /// <param name="wordA">
        /// The word a.
        /// </param>
        /// <param name="wordB">
        /// The word b.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int Similarity(Word wordA, Word wordB)
        {
            var lenA = wordA.PhoneticLength;
            var lenB = wordB.PhoneticLength;
            var sgmax = 0;
            int m4 = Constants.NoScore, m5 = Constants.NoScore;

            this.ScoreMatrix[0][0] = 0;

            for (var i = 1; i <= lenA; i++)
            {
                this.ScoreMatrix[i][0] = 0;
            }

            for (var j = 1; j <= lenB; j++)
            {
                this.ScoreMatrix[0][j] = 0;
            }

            for (short i = 1; i <= lenA; i++)
            {
                for (short j = 1; j <= lenB; j++)
                {
                    var m1 = this.Score(i - 1, j) + Sigma.Skip(this.Parameters.SkipCost);
                    var m2 = this.Score(i, j - 1) + Sigma.Skip(this.Parameters.SkipCost);
                    var m3 = this.Score(i - 1, j - 1) + Sigma.Sub(wordA, i, wordB, j, this.Parameters);

                    if (this.Parameters.Squashing)
                    {
                        m4 = this.Score(i - 1, j - 2) + Sigma.Expand(wordA, i, wordB, j - 1, j, this.Parameters);
                        m5 = this.Score(i - 2, j - 1) + Sigma.Expand(wordB, j, wordA, i - 1, i, this.Parameters);
                    }

                    var lmax = this.Max(m1, m2, m3, m4, m5, 0);

                    this.ScoreMatrix[i][j] = lmax;

                    if (lmax > sgmax)
                    {
                        sgmax = lmax;
                    }
                }
            }

            return sgmax;
        }
    }
}
