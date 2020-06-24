namespace Catmash.Algorithms
{
    /// <summary>
    ///     The common interface to be implemented by indices pairs generators.
    /// </summary>
    public interface IPairGeneratorStrategy
    {
        /// <summary>
        ///     For N elements, the set of possible pairs has cardinality N*(N-1).
        ///     Each pair is assigned a number in range [0, N*(N-1)[ and GetPair
        ///     returns the kth pair of indices. Each index is in range [0, N-1].
        /// </summary>
        /// <param name="pairNumber"></param>
        ///     A number that identifies a pair of indices in range nbElements*(nbElements-1).
        /// <param name="nbElements"></param>
        ///     The number of elements.
        /// <returns>
        ///     A tuple representing the pair of indices.
        /// </returns>

        (int, int) GetPair(int pairNumber, int nbElements);
    }
}