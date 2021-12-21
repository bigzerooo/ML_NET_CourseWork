using Microsoft.ML.Data;

namespace Trips.ML.API.Models
{
    public class TripRating
    {
        [LoadColumn(0)]
        public float UserId { get; set; }

        [LoadColumn(1)]
        public float TripId { get; set; }

        [LoadColumn(2)]
        public float Label { get; set; }
    }
}
