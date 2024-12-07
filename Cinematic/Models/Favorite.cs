namespace Cinematic.Models
{
    public class Favorite
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
