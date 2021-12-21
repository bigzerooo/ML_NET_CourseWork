namespace Trips.Data.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Tag> Tags { get; set; }

        public List<Rating> Ratings { get; set; }
    }
}
