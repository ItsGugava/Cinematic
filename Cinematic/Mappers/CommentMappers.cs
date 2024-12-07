using Cinematic.Dtos.CommentDtos;
using Cinematic.Models;
using Microsoft.AspNetCore.Identity;

namespace Cinematic.Mappers
{
    public static class CommentMappers
    {
        public static Comment FromCreateDtoToComment(this CreateCommentRequestDto comment, string appUserId)
        {
            return new Comment
            {
                Text = comment.Text,
                MovieId = comment.MovieId,
                AppUserId = appUserId                
            };
        }

        public static CommentDto FromCommentToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Text = comment.Text,
                DateTime = comment.DateTime,
                MovieId = comment.MovieId,
                CreatedBy = comment.AppUser.UserName
            };
        }
    }
}
