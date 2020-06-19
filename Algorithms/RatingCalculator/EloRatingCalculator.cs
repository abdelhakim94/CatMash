using System;

namespace Catmash.Algorithms
{
    /// <summary>
    ///     The Elo rating system implementation.
    /// </summary>

    public class EloRatingCalculator
    {
        /// <summary>
        ///     Computes the expectation of a player with rating ratingA
        ///     to win against another player with rating ratingB, as
        ///     defined by the basic Elo rating system.
        /// </summary>
        /// <param name="ratingA"></param>
        ///     Elo rating of first player
        /// <param name="ratingB"></param>
        ///     Elo rating of second player
        /// <returns> The expectation of first player wining againt second player
        /// </returns>

        public double ComputeExpectation(double ratingA, double ratingB)
        {
            return (1 / (1 + Math.Pow(10, (ratingB - ratingA) / 400)));
        }

        /// <summary>
        ///     Computes the new rating of a player as defined in the basic Elo
        ///     rating system.
        /// </summary>
        /// <param name="rating"></param>
        ///     The current Elo rating of the player
        /// <param name="expected"></param>
        ///     The expected score of the player
        /// <param name="actual"></param>
        ///     The actual score of the player
        /// <param name="kFactor"></param>
        ///     The Elo k factor
        /// <returns></returns>

        public double ComputeRating(double rating, double expected, double actual, double kFactor)
        {
            return (rating + kFactor * (actual - expected));
        }
    }
}