using Cinematic.Dtos.MovieDtos;
using Cinematic.Extensions;
using Cinematic.Interfaces;
using Cinematic.Mappers;
using Cinematic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinematic.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        private readonly IMovieRepository _movieRepo;
        public MovieController(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Movie> movies = await _movieRepo.GetAllAsync();
            List<MovieDto> moviesDto = movies.Select(m => m.FromMovieToMovieDto()).ToList();
            return Ok(moviesDto);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Movie? movie = await _movieRepo.GetByIdAsync(id);
            if(movie == null)
            {
                return NotFound();
            }
            return Ok(movie.FromMovieToMovieDto());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequestDto movieDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Movie movie = movieDto.FromCreateDtoToMovie();

            await _movieRepo.CreateAsync(movie);
            return CreatedAtAction(nameof(GetById), new {id  = movie.Id}, movie);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMovieRequestDto movieDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Movie? movie = await _movieRepo.UpdateAsync(id, movieDto);
            if(movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Movie? movie = await _movieRepo.DeleteAsync(id);
            if(movie == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
