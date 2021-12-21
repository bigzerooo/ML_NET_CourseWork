using Microsoft.AspNetCore.Mvc;
using Trips.Data.Models.Trip;
using Trips.ML.API.Models;
using Trips.ML.Interfaces;

namespace Trips.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        IPredictingService _predictingService;

        public PredictionController(IPredictingService predictingService)
        {
            _predictingService = predictingService;
        }

        [HttpPost("[action]")]
        public IActionResult Predict(TripRating tripRating)
        {
            var prediction = _predictingService.PredictRating(tripRating);

            return Ok(prediction);
        }

        [HttpGet("[action]/{userId}")]
        public ActionResult<List<TripModel>> GetRecommendedForUser(int userId)
        {
            var trips = _predictingService.GetRecommendedForUser(userId);

            return Ok(trips);
        }
    }
}
