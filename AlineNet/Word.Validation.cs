namespace AlineNet
{
    using Exceptions;

    internal partial class Word
    {
        /// <summary>
        /// Validates a string by determining if it's a valid word.
        /// </summary>
        /// <param name="wordText">
        /// The word text.
        /// </param>
        /// <exception cref="InvalidWordException">
        /// </exception>
        /// <exception cref="MaxLengthExceededException">
        /// </exception>
        // ReSharper disable once StyleCop.SA1627
        private static void Validate(string wordText)
        {
            if(string.IsNullOrEmpty(wordText) 
                || string.IsNullOrWhiteSpace(wordText) 
                || wordText.Contains(" ")
                || wordText.Contains("-"))
            {
                throw new InvalidWordException("An empty or invalid word was provided.");
            }

            if (wordText.Length > Constants.Elen)
            {
                throw new MaxLengthExceededException($"Max word representation length ({Constants.Elen}) exceeded.");
            }
        }
    }
}
