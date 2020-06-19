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
            double ratingA = 123.4;
            double ratingB = 865.23;
            double expectation = 1 / (1 + Math.Pow(10, (ratingB - ratingA) / 400));
            EloRatingCalculator ratingCalculator = new EloRatingCalculator();

            // Act
            double computedExpectation = ratingCalculator.ComputeExpectation(ratingA, ratingB);

            Assert.Equal(expectation, computedExpectation);
        }
    }
}