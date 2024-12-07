namespace Cinematic.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public double Rating { get; set; }
        public List<Comment> Comments {  get; set; } = new List<Comment>();
        public List<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
