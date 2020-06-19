using Xunit;
using System.Linq;
using Catmash.Algorithms;
using System.Collections.Generic;

namespace Catmash.Tests.Algorithms
{
    public class PatternedPairGeneratorTest
    {
        private List<(int, int)> GetAllPairs(int nbElements)
        {
            List<(int, int)> allPairs = new List<(int, int)>();
            for (int i = 0; i < nbElements; i++)
            {
                for (int j = 0; j < nbElements; j++)
                {
                    if (i != j) allPairs.Add((i, j));
                }
            }
            return allPairs;
        }
        [Fact]
        public void GetPair_PairNumberAndNbElementsGiven_ShouldReturnAllPairs()
        {
            // Arrange
            int nbElements = 10;
            int possiblePairs = nbElements * (nbElements - 1);
            IPairGeneratorStrategy pairGenerator = new PatternedPairGenerator();
            List<(int, int)> generatedPairs = new List<(int, int)>();

            // Act
            for (int i = 0; i < possiblePairs; i++)
            {
                generatedPairs.Add(pairGenerator.GetPair(i, nbElements));
            }
            generatedPairs = generatedPairs.OrderBy(p => p.Item2).OrderBy(p => p.Item1).ToList();

            //Assert
            Assert.Equal(GetAllPairs(nbElements), generatedPairs);
        }
    }
}
