using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trips.Data.Context;
using Trips.Data.Entities;
using Trips.Data.Inferfaces;
using Trips.Data.Models.Trip;

namespace Trips.Data.Services
{
    public class TripService : BaseService, ITripService
    {
        public TripService(TripDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task CreateTripAsync(TripModel trip)
        {
            var tripEntity = _mapper.Map<Trip>(trip);

            _dbContext.Add(tripEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TripModel>> GetTripsAsync()
        {
            var trips = await _dbContext.Trips.ToListAsync();

            return _mapper.Map<List<TripModel>>(trips);
        }
    }
}
