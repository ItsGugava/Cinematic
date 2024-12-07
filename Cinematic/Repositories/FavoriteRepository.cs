using Cinematic.Data;
using Cinematic.Interfaces;
using Cinematic.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinematic.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMovieRepository _movieRepo;
        public FavoriteRepository(ApplicationDbContext context, IMovieRepository movieRepo)
        {
            _context = context;
            _movieRepo = movieRepo;
        }

        public async Task<Favorite> CreateAsync(Favorite favorite)
        {
            bool movieFound = _movieRepo.MovieFound(favorite.MovieId);
            if(!movieFound)
            {
                return null;
            }
            await _context.Favorites.AddAsync(favorite);
            await _context.SaveChangesAsync();
            return favorite;
        }

        public async Task<Favorite?> DeleteAsync(Favorite favorite)
        {
            Favorite? favoriteResult = await _context.Favorites.FirstOrDefaultAsync(f => f.MovieId == favorite.MovieId && f.AppUserId == favorite.AppUserId);
            if(favoriteResult == null)
            {
                return null;
            }
            _context.Favorites.Remove(favoriteResult);
            await _context.SaveChangesAsync();
            return favoriteResult;
        }

        public async Task<List<Movie>> GetAllAsync(string appUserId)
        {
            List<Favorite> favorites = await _context.Favorites.Where(f => f.AppUserId == appUserId).Include(f => f.Movie).ToListAsync();
            List<Movie> movies = favorites.Select(f => f.Movie).ToList();
            return movies;
        }
    }
}
