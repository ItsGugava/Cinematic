namespace Cinematic.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser {  get; set; }
    }
}
