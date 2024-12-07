using Cinematic.Dtos.MovieDtos;
using Cinematic.Models;

namespace Cinematic.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllAsync();    
        Task<Movie?> GetByIdAsync(int id);
        Task<Movie> CreateAsync(Movie movie);
        Task<Movie?> UpdateAsync(int id, UpdateMovieRequestDto movieDto);
        Task<Movie?> DeleteAsync(int id);
        bool MovieFound(int id);
    }
}
