using Cinematic.Dtos.CommentDtos;
using Cinematic.Models;

namespace Cinematic.Dtos.MovieDtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public double Rating { get; set; }
        public List<CommentDto> CommentDtos { get; set; } = new List<CommentDto>();
    }
}
