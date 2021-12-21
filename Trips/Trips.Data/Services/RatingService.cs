using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trips.Data.Context;
using Trips.Data.Entities;
using Trips.Data.Inferfaces;
using Trips.Data.Models;

namespace Trips.Data.Services
{
    public class RatingService : BaseService, IRatingService
    {
        public RatingService(TripDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<List<RatingModel>> GetRatingAsync()
        {
            var rating = await _dbContext.Rating.ToListAsync();

            return _mapper.Map<List<RatingModel>>(rating);
        }

        public async Task RateTripAsync(RatingModel ratingModel)
        {
            var rating = _mapper.Map<Rating>(ratingModel);
            _dbContext.Add(rating);

            await _dbContext.SaveChangesAsync();
        }
    }
}
