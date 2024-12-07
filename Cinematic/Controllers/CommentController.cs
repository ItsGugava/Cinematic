using Azure.Identity;
using Cinematic.Dtos.CommentDtos;
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
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string appUserId = User.GetId();
            Comment comment = commentDto.FromCreateDtoToComment(appUserId);
            Comment? commentResult = await _commentRepo.CreateAsync(comment);
            if(commentResult == null)
            {
                return NotFound("MovieId not found");
            }
            return Ok(await _commentRepo.GetByIdAsync(commentResult.Id));
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string appUserId = User.GetId();
            Comment commentResult = await _commentRepo.UpdateAsync(id,  commentDto, appUserId);
            if(commentResult == null)
            {
                return StatusCode(500, "Comment id not found or wrong user");
            }
            return Ok(await _commentRepo.GetByIdAsync(commentResult.Id));
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            string appUserId = User.GetId();
            Comment? commentResult = await _commentRepo.DeleteAsync(id, appUserId);
            if (commentResult == null)
            {
                return StatusCode(500, "Comment id not found or wrong user");
            }
            return NoContent();
        }
    }
}
