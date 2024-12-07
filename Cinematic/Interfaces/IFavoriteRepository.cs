using Cinematic.Models;

namespace Cinematic.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<Favorite?> CreateAsync(Favorite favorite);
        Task<Favorite?> DeleteAsync(Favorite favorite);
        Task<List<Movie>> GetAllAsync(string appUserId);
    }
}
