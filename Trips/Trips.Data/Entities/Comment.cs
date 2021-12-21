namespace Trips.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Value { get; set; }

        public int TripId { get; set; }
        public Trip Trip { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
