namespace Trips.Data.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public List<Trip> Trips { get; set; }
    }
}
