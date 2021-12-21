using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trips.Data.Models;

namespace Trips.Data.Inferfaces
{
    public interface IRatingService
    {
        Task RateTripAsync(RatingModel ratingModel);
        Task<List<RatingModel>> GetRatingAsync();
    }
}
