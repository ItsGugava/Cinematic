using System.ComponentModel.DataAnnotations;

namespace Cinematic.Dtos.MovieDtos
{
    public class UpdateMovieRequestDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        [Range(0.1, 10)]
        public double Rating { get; set; }
    }
}
