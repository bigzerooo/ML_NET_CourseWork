using Trips.Data.Models.Trip;

namespace Trips.Data.Inferfaces
{
    public interface ITripService
    {
        public Task<List<TripModel>> GetTripsAsync();

        public Task CreateTripAsync(TripModel trip);
    }
}
