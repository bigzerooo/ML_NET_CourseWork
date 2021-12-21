using AutoMapper;
using Microsoft.Extensions.ML;
using Trips.Data.Context;
using Trips.Data.Models.Trip;
using Trips.Data.Services;
using Trips.ML.API.Models;
using Trips.ML.Interfaces;

namespace Trips.ML.Services
{
    public class PredictingService : BaseService, IPredictingService
    {
        private readonly PredictionEnginePool<TripRating, TripRatingPrediction> _model;

        public PredictingService(PredictionEnginePool<TripRating, TripRatingPrediction> model,
            TripDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _model = model;
        }

        public List<TripWithRatingModel> GetRecommendedForUser(int userId)
        {
            var allTripsIds = _dbContext.Rating.Select(t => t.TripId).Distinct();
            var ratedTrips = _dbContext.Rating.Where(r => r.UserId == userId).Select(t => t.TripId);

            if (!ratedTrips.Any())
            {
                return new List<TripWithRatingModel>();
            }

            var unratedTrips = allTripsIds.Except(ratedTrips).Join(_dbContext.Trips, r => r, t => t.Id, (r, t) => t).ToList();

            var mappedTrips = _mapper.Map<List<TripWithRatingModel>>(unratedTrips);

            foreach (var trip in mappedTrips)
            {
                var prediction = PredictRating(new TripRating { UserId = userId, TripId = trip.Id });
                trip.Rating = prediction.Score;
            }

            return mappedTrips;
        }

        public TripRatingPrediction PredictRating(TripRating tripRating)
        {
            var prediction = _model.Predict(tripRating);
            return prediction;
        }
    }
}
