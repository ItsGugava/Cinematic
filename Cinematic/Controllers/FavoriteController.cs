using Cinematic.Dtos.MovieDtos;
using Cinematic.Extensions;
using Cinematic.Interfaces;
using Cinematic.Mappers;
using Cinematic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinematic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepo;
        public FavoriteController(IFavoriteRepository favoriteRepo)
        {
            _favoriteRepo = favoriteRepo;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("{movieId}")]
        public async Task<IActionResult> Create([FromRoute] int movieId)
        {
            string appUserId = User.GetId();
            Favorite favorite = new Favorite { AppUserId =  appUserId, MovieId = movieId };
            Favorite? favoriteResult = await _favoriteRepo.CreateAsync(favorite);
            if (favoriteResult == null)
            {
                return NotFound("Movie not found.");
            }
            return Ok("Movie was added to favorites.");
        }

        [HttpDelete]
        [Authorize(Roles = "User")]
        [Route("{movieId}")]
        public async Task<IActionResult> Delete([FromRoute] int movieId)
        {
            string appUserId = User.GetId();
            Favorite favorite = new Favorite { AppUserId = appUserId, MovieId = movieId };
            Favorite? favoriteResult = await _favoriteRepo.DeleteAsync(favorite);
            if(favoriteResult == null)
            {
                return NotFound("Movie not found in favorites list");
            }
            return Ok("Movie was removed from favorites list");
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAll()
        {
            string appUserId = User.GetId();
            List<Movie> movies = await _favoriteRepo.GetAllAsync(appUserId);
            List<MovieDto> moviesDtos = movies.Select(m => m.FromMovieToMovieDto()).ToList();
            return Ok(moviesDtos);
        }
    }
}
