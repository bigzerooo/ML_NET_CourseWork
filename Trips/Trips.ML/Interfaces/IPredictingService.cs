using Trips.Data.Models.Trip;
using Trips.ML.API.Models;

namespace Trips.ML.Interfaces
{
    public interface IPredictingService
    {
        public TripRatingPrediction PredictRating(TripRating tripRating);

        public List<TripWithRatingModel> GetRecommendedForUser(int userId);
    }
}
