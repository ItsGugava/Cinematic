using Cinematic.Dtos.CommentDtos;
using Cinematic.Models;

namespace Cinematic.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment?> CreateAsync(Comment comment);
        Task<CommentDto?> GetByIdAsync(int id);
        Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto, string appUserId);
        Task<Comment?> DeleteAsync(int id, string appUserId);
    }
}
