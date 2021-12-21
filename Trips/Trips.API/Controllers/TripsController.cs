#nullable disable
using Microsoft.AspNetCore.Mvc;
using Trips.Data.Inferfaces;
using Trips.Data.Models.Trip;

namespace Trips.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        // GET: api/Trips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripModel>>> GetTrips()
        {
            return await _tripService.GetTripsAsync();
        }

        // POST: api/Trips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TripModel>> PostTrip(TripModel trip)
        {
            await _tripService.CreateTripAsync(trip);

            return Ok();
        }
    }
}
