using Cinematic.Dtos.MovieDtos;
using Cinematic.Models;
using System.Runtime.CompilerServices;

namespace Cinematic.Mappers
{
    public static class MovieMappers
    {
        public static Movie FromCreateDtoToMovie(this CreateMovieRequestDto movieDto)
        {
            return new Movie
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                Genre = movieDto.Genre,
                Rating = movieDto.Rating
            };
        }

        public static MovieDto FromMovieToMovieDto(this Movie movie)
        {
            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                Rating = movie.Rating,
                CommentDtos = movie.Comments.Select(c => c.FromCommentToCommentDto()).ToList()
            };
        }
    }
}
