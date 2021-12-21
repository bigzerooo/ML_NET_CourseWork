namespace Trips.Data.Models
{
    public class RatingModel
    {
        public int UserId { get; set; }
        public int TripId { get; set; }
        public int Value { get; set; }
    }
}
