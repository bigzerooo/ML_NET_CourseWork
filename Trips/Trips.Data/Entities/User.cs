namespace Trips.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Comment> LeavedComments { get; set; }

        public List<Rating> Ratings { get; set; }
    }
}
