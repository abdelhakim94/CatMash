using System;
using Xunit;
using System.Linq;
using Catmash.Algorithms;
using System.Collections.Generic;

namespace Catmash.Tests.Algorithms
{
    public class EloRatingCalculatorTest
    {
        [Fact]
        public void ComputeExpectation_ratingsGiven_ShouldReturnExpectation()
        {
            // Arrange
            decimal ratingA = 123.4M;
            decimal ratingB = 865.23M;
            decimal expectation = (decimal)(1 / (1 + Math.Pow(10, (double)((ratingB - ratingA) / 400))));
            EloRatingCalculator ratingCalculator = new EloRatingCalculator();

            // Act
            decimal computedExpectation = ratingCalculator.ComputeExpectation(ratingA, ratingB);

            // Assert
            Assert.Equal(expectation, computedExpectation);
        }

        [Fact]
        public void ComputeRating_CurrentRatingAndExpectedAndActualScoresAndKFactorGiven_ShouldReturnNewRating()
        {
            // Arrange
            decimal currentRating = 757.34M;
            decimal expectedScore = 0.74M;
            decimal actualScore = 1;
            decimal kFactor = 24;

            decimal newRating = currentRating + kFactor * (actualScore - expectedScore);
            EloRatingCalculator ratingCalculator = new EloRatingCalculator();

            // Act
            decimal computedRating = ratingCalculator.ComputeRating(
                currentRating,
                expectedScore,
                actualScore,
                kFactor);

            // Assert
            Assert.Equal(newRating, computedRating);
        }
    }
}