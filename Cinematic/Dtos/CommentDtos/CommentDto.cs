using Cinematic.Models;

namespace Cinematic.Dtos.CommentDtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int MovieId { get; set; }
        public string CreatedBy {  get; set; }
    }
}
