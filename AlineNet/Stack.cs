// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Stack.cs" company="">
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
//   A custom stack for storing the alignments
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AlineNet
{
    /// <summary>
    /// A custom stack for storing the alignments
    /// </summary>
    internal class Stack
    {
        /// <summary>
        /// Indexer for the stack
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int[] this[int index] => stack[index];

        /// <summary>
        /// trace description
        /// </summary>
        private readonly int[][] stack;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stack"/> class.
        /// </summary>
        public Stack()
        {
            this.stack = new int[2][];
            this.stack[0] = new int[Constants.Maxl];
            this.stack[1] = new int[Constants.Maxl];
        }

        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        internal int Top { get; set; }

        /// <summary>
        /// Returns the topmost element of the stack and removes it from the list
        /// </summary>
        /// <param name="k">
        /// </param>
        public void Pop(int k = 1)
        {
            Top -= k;
        }

        /// <summary>
        /// The push.
        /// </summary>
        /// <param name="i1">
        /// The i 1.
        /// </param>
        /// <param name="i2">
        /// The i 2.
        /// </param>
        public void Push(int i1, int i2 = Constants.Nul)
        {
            this.stack[0][this.Top] = i1;
            this.stack[1][this.Top] = i2;
            this.Top++;
        }

        /// <summary>
        /// The clear.
        /// </summary>
        public void Clear()
        {
            Top = 0;
            foreach (var t in this.stack)
            {
                for (var j = 0; j < t.Length; j++)
                {
                    t[j] = Constants.Nul;
                }
            }
        }

        /// <summary>
        /// The alignment.
        /// </summary>
        /// <param name="word">
        /// The word.
        /// </param>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="aback">
        /// The aback.
        /// </param>
        internal void Alignment(Word word, short row, char[] aback)
        {
            var abackcount = 0;

            for (var ind = 0; ind < Top; ind++)
            {
                var i = this.stack[row][ind];

                if (abackcount >= aback.Length)
                {
                    break;
                }

                switch (i)
                {
                    case Constants.Nul: 
                        aback[abackcount] = '-';
                        abackcount++;
                        if (abackcount >= aback.Length)
                        {
                            break;
                        }
                            
                        aback[abackcount] = ' ';
                        abackcount++;

                        if (abackcount >= aback.Length)
                        {
                            break;
                        }

                        aback[abackcount] = ' ';
                        abackcount++;

                        if (abackcount >= aback.Length)
                        {
                            break;
                        }

                        aback[abackcount] = ' ';
                        abackcount++;
                        break;
                    case Constants.Lim:
                        aback[abackcount] = '|';
                        abackcount++;

                        if (abackcount >= aback.Length)
                        {
                            break;
                        }

                        aback[abackcount] = ' ';
                        abackcount++;

                        if (abackcount >= aback.Length)
                        {
                            break;
                        }

                        aback[abackcount] = ' ';
                        abackcount++;
                        break;
                    case Constants.Dub:
                        aback[abackcount] = '<';
                        abackcount++;

                        if (abackcount >= aback.Length)
                        {
                            break;
                        }

                        aback[abackcount] = ' ';
                        abackcount++;

                        if (abackcount >= aback.Length)
                        {
                            break;
                        }

                        aback[abackcount] = ' ';
                        abackcount++;

                        if (abackcount >= aback.Length)
                        {
                            break;
                        }

                        aback[abackcount] = ' ';
                        abackcount++;
                        break;
                    default:
                        abackcount = word.Flush(word.PhoneticLength - i, aback, abackcount);
                        break;
                }

                if (abackcount < aback.Length)
                { 
                    aback[abackcount] = '\0';
                }
            }
        }
    }
}
