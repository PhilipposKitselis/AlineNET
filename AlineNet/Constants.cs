// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="">
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
//   The constants from the C++ code.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AlineNet
{
    /// <summary>
    /// The constants from the C++ code.
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// The no score.
        /// </summary>
        public const int NoScore = -99999;

        // Globals from the C++ code
        public const int FtLen = 13;
        public const int Maxl = 40;                 // max length of an input word
        public const int LineLen = 800;             // max input line length
        public const int MaxInputSize = 2400;       // max input size (in lines)
        public const int BaseUpp = 65;             // = ord('A')
        public const int BaseLow = 97;             // = ord('a')
        public const int Offset = 32;               // = ord('a') - ord('A')
        public const int Nul = -1;                  // empty alignment
        public const int Lim = -2;                  // border character (for local comparison)
        public const int Dub = -3;                  // double linking (for compression)
        public const int Tab = 4;                   // output space for one phoneme
        public const int Plen = 42;                 // max word phonemic length (+2)
        public const int Elen = 30;                 // max word representation length
        public const int Nseg = 26;                 // number of recognized segments

        // feature values
        public const int FSyl = 0;
        public const short Syl = 100;
        public const short Nsl = 0;
        public const int FPlace = 1;
        public const short Glo = 10;

        //	public const short pha = 30;
        //  public const short uvu = 50;
        public const short Vel = 60;
        public const short Pal = 70;
        public const short Pav = 75;
        public const short Rfl = 80;
        public const short Alv = 85;
        public const short Dnt = 90;
        public const short Lbd = 95;
        public const short Blb = 100;
        public const int FStop = 2;
        public const short Lvl = 0;
        public const short Mvl = 20;
        public const short Hvl = 40;
        public const short Vwl = Hvl;
        public const short Apr = 60;
        public const short Frc = 80;
        public const short Afr = 90;
        public const short Stp = 100;
        public const int FVoice = 3;
        public const short Vce = 100;
        public const short Vls = 0;
        public const int FNasal = 4;
        public const short Nas = 100;
        //	public const short nns = 0;
        public const int FRetro = 5;
        public const short Ret = 100;
        //	public const short nrt = 0;
        public const int FLat = 6;
        public const short Lat = 100;
        //	public const short nlt = 0;
        public const int FAsp = 7;
        public const short Asp = 100;
        //	public const short nap = 0;
        public const int FLong = 8;
        public const short Lng = 100;
        //	public const short sht = 0;
        public const int FHigh = 9;
        public const short Hgh = 100;
        public const short Mid = 50;
        public const short Low = 0;
        public const int FBack = 10;
        public const short Frt = 100;
        public const short Cnt = 50;
        public const short Bak = 0;
        public const int FRound = 11;
        public const short Rnd = 100;
        //	public const short nrd = 0;
        public const int FDouble = 12;
    }
}
