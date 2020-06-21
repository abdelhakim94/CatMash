namespace Catmash.Algorithms
{
    /// <summary>
    ///     A generator of indices pairs that mimics a random behavior
    /// </summary>

    public class PatternedPairGenerator : IPairGeneratorStrategy
    {
        /// <summary>
        ///     For N elements, the set of possible pairs has cardinality N*(N-1).
        ///     Each pair is assigned a number in range [0, N*(N-1)[ and GetPair
        ///     returns the kth pair of indices. Each index is in range [0, N-1].
        ///     The indices calculation follows a pattern that mimics randomness
        ///     enough so as not to have indices that come up often in successive
        ///     calls.
        /// </summary>
        /// <param name="pairNumber"></param>
        ///     A number in range nbElements*(nbElements-1) that identifies a pair
        ///     of indices.
        /// <param name="nbElements"></param>
        ///     The number of elements.
        /// <returns>A tuple representing the pair of indices calculated</returns>

        public (int, int) GetPair(int pairNumber, int nbElements)
        {
            int possiblePairs = nbElements * (nbElements - 1);
            pairNumber %= possiblePairs;

            int positionInSlice = pairNumber % (nbElements - 1);
            int newPairNumber = ((pairNumber + (nbElements - 1) * positionInSlice + (nbElements / 2))) % possiblePairs;

            int firstIndex = newPairNumber / (nbElements - 1);
            int secondIndex = newPairNumber % (nbElements - 1);
            if (secondIndex >= firstIndex) secondIndex++;

            return (firstIndex, secondIndex);
        }
    }
}