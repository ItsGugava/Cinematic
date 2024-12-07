using Cinematic.Data;
using Cinematic.Dtos.MovieDtos;
using Cinematic.Interfaces;
using Cinematic.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinematic.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie?> DeleteAsync(int id)
        {
            Movie? movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if(movie == null)
            {
                return null;
            }
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            List<Movie> movies = await _context.Movies.Include(m => m.Comments).ThenInclude(c => c.AppUser).ToListAsync();
            return movies;
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            Movie? movie = await _context.Movies.Include(m => m.Comments).ThenInclude(c => c.AppUser).FirstOrDefaultAsync(x => x.Id == id);
            return movie;
        }

        public bool MovieFound(int id)
        {
            bool found = _context.Movies.Any(m => m.Id == id);
            return found;
        }

        public async Task<Movie?> UpdateAsync(int id, UpdateMovieRequestDto movieDto)
        {
            Movie? movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if(movie == null)
            {
                return null;
            }
            movie.Title = movieDto.Title;
            movie.Description = movieDto.Description;
            movie.Genre = movieDto.Genre;
            movie.Rating = movieDto.Rating;
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
