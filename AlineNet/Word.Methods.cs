namespace AlineNet
{
    using System.Collections.Generic;
    using ExtensionMethods;

    /// <summary>
    /// The word class.
    /// </summary>
    internal partial class Word
    {
        /// <summary>
        /// The flush.
        /// </summary>
        /// <param name="pos">
        /// The position.
        /// </param>
        /// <param name="aback">
        /// The aback.
        /// </param>
        /// <param name="abackcount">
        /// The abackcount.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        // ReSharper disable once StyleCop.SA1650
        internal int Flush(int pos, char[] aback, int abackcount)
        {
            var current = this.ind[pos];
            var next = this.ind[pos + 1];

            for (var t = 0; t < Constants.Tab && abackcount < aback.Length; t++)
            {
                if (current != next)
                {
                    current++;
                    aback[abackcount] = this.wordArrayCopy[current - 1];
                    abackcount++;
                }
                else
                {
                    aback[abackcount] = ' ';
                    abackcount++;
                }
            }

            return abackcount;
        }

        /// <summary>
        /// Applies the redundancy rules.
        /// </summary>
        private static void ApplyRedundancyRules()
        {
            for (var i = 0; i < Constants.Nseg; i++)
            {
                if (IndividualMatrix[i].IsVowel())
                {
                    switch (IndividualMatrix[i][Constants.FHigh])
                    {
                        case Constants.Hgh:
                            IndividualMatrix[i][Constants.FStop] = Constants.Hvl;
                            break;
                        case Constants.Mid:
                            IndividualMatrix[i][Constants.FStop] = Constants.Mvl;
                            break;
                        case Constants.Low:
                            IndividualMatrix[i][Constants.FStop] = Constants.Lvl;
                            break;
                    }
                }

                if (!IndividualMatrix[i].IsVowel())
                {
                    IndividualMatrix[i][Constants.FHigh] = Constants.Hgh;
                }
            }
        }

        /// <summary>
        /// The create feature matrix.
        /// </summary>
        private void CreateFeatureMatrix()
        {
            var j = -1;
            for (var seg = 0; seg < this.Length; seg++)
            {
                var c = Helpers.RemoveDiacritics(this.wordArrayCopy[seg]);

                if (c >= Constants.BaseLow)
                {
                    j++;
                    for (var f = 0; f < Constants.FtLen; f++)
                    {
                        if (this.featureMatrix[j] == null)
                        {
                            this.featureMatrix[j] = new int[Constants.Plen];
                        }

                        this.featureMatrix[j][f] = IndividualMatrix[c - Constants.BaseLow][f];
                    }

                    this.ind[j] = seg;
                }
                else if (c == 0)
                {
                    j++;
                    for (var f = 0; f < Constants.FtLen; f++)
                    {
                        if (this.featureMatrix[j] == null)
                        {
                            this.featureMatrix[j] = new int[Constants.Plen];
                        }

                        this.featureMatrix[j][f] = 0;
                    }

                    this.ind[j] = seg;
                }
                else
                {
                    if (j < 0)
                    {
                        j = 0;
                    }

                    this.Modify(this.featureMatrix[j], c);
                }
            }

            this.ind[j + 1] = this.Length;
            this.PhoneticLength = j + 1;
        }

        /// <summary>
        /// The modify.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <param name="c">
        /// The c.
        /// </param>
        private void Modify(IList<int> p, char c)
        {
            switch (c)
            {
                case 'A':
                    p[Constants.FAsp] = Constants.Asp;        // "Aspirated"
                    break;
                case 'C':
                    p[Constants.FBack] = Constants.Cnt;       // "Central"
                    break;
                case 'D':
                    p[Constants.FPlace] = Constants.Dnt;      // "Dental"
                    break;
                case 'F':
                    p[Constants.FBack] = Constants.Frt;       // "Front"
                    break;
                case 'H':
                    p[Constants.FLong] = Constants.Lng;       // "loHng"
                    break;
                case 'L':                       // "Lax" (ignored)
                    break;
                case 'N':
                    p[Constants.FNasal] = Constants.Nas;      // "Nasal"
                    break;
                case 'P':
                    p[Constants.FPlace] = Constants.Pal;      // "Palatal"
                    break;
                case 'S':
                    p[Constants.FStop] = Constants.Frc;       // "Spirant"
                    break;
                case 'V':
                    p[Constants.FPlace] = Constants.Pav;      // "palato-alVeolar"
                    break;
                case 'X':
                    p[Constants.FRetro] = Constants.Ret;      // "retrofleX" added by SD 8-31-06
                    break;
            }
        }
    }
}
