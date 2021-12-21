using Microsoft.AspNetCore.Mvc;
using Trips.Data.Inferfaces;
using Trips.Data.Models;

namespace Trips.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        // GET: api/Rating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingModel>>> GetRating()
        {
            return await _ratingService.GetRatingAsync();
        }

        // POST: api/Rating
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RatingModel>> RateTrip(RatingModel rating)
        {
            await _ratingService.RateTripAsync(rating);

            return Ok();
        }
    }
}
