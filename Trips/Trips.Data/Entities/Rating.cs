namespace Trips.Data.Entities
{
    public class Rating
    {
        public int Value { get; set; }

        public int TripId { get; set; }
        public Trip Trip { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
