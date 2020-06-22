using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catmash.DataRepository;
using Catmash.Algorithms;
using Catmash.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Catmash.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class CatmashImageController : ControllerBase
    {
        private readonly ICatmashRepository repository;
        private readonly IPairGeneratorStrategy pairGenerator;
        private readonly PairNumberTracker pairNumTracker;
        private readonly EloRatingCalculator eloRatingCalculator;
        private readonly Constants constants;

        public CatmashImageController(ICatmashRepository repository,
            IPairGeneratorStrategy pairGenerator,
            PairNumberTracker pairNumTracker,
            EloRatingCalculator eloRatingCalculator,
            Constants constants)
        {
            this.repository = repository;
            this.pairGenerator = pairGenerator;
            this.pairNumTracker = pairNumTracker;
            this.eloRatingCalculator = eloRatingCalculator;
            this.constants = constants;
        }

        [HttpGet("pair")]
        [ProducesResponseType(200, Type = typeof(ImagePair))]
        public async Task<ImagePair> GetPair()
        {
            var pair = pairGenerator.GetPair(pairNumTracker.NextPair(),
                await repository.CountAsync());
            var firstImage = await repository.RetrieveAsync(pair.Item1);
            var secondImage = await repository.RetrieveAsync(pair.Item2);
            return new ImagePair { first = firstImage, second = secondImage };
        }

        [HttpPost("score/{winnerId}/{loserId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateScores(string winnerId, string loserId)
        {
            var winnerImage = await repository.RetrieveAsync(winnerId);
            var loserImage = await repository.RetrieveAsync(loserId);
            if (winnerImage is null || loserImage is null)
                return NotFound();

            var expectedScoreWinner = eloRatingCalculator.ComputeExpectation(
                winnerImage.Score, loserImage.Score);
            var expectedScoreLoser = eloRatingCalculator.ComputeExpectation(
                loserImage.Score, winnerImage.Score);

            winnerImage.Score = eloRatingCalculator.ComputeRating(
                winnerImage.Score, expectedScoreWinner, constants.ScoreWhenWin, constants.KFactor);
            loserImage.Score = eloRatingCalculator.ComputeRating(
                loserImage.Score, expectedScoreLoser, constants.ScoreWhenLose, constants.KFactor);

            winnerImage.Votes++;

            bool? success = await repository.SaveChangesAsync();
            if (success is null)
                return StatusCode(StatusCodes.Status500InternalServerError);
            else
                return Ok();
        }
    }
}
