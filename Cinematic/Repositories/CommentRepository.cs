using Cinematic.Data;
using Cinematic.Dtos.CommentDtos;
using Cinematic.Interfaces;
using Cinematic.Mappers;
using Cinematic.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinematic.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IMovieRepository _movieRepo;
        private readonly ApplicationDbContext _context;
        public CommentRepository(IMovieRepository movieRepo, ApplicationDbContext context)
        {
            _movieRepo = movieRepo;
            _context = context;
        }

        public async Task<Comment?> CreateAsync(Comment comment)
        {
            if(!_movieRepo.MovieFound(comment.MovieId))
            {
                return null;
            }
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id, string appUserId)
        {
            Comment? comment = await _context.Comments.FirstOrDefaultAsync(c  => c.Id == id);
            if (comment == null)
            {
                return null;
            }
            if(!comment.AppUserId.Equals(appUserId))
            {
                return null;
            }
            _context.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<CommentDto?> GetByIdAsync(int id)
        {
            Comment? comment = await _context.Comments.Include(c => c.AppUser).FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return null;
            }
            CommentDto commentDto = comment.FromCommentToCommentDto();
            return commentDto;
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentDto, string appUserId)
        {
            Comment? comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return null;
            }
            if(!comment.AppUserId.Equals(appUserId))
            {
                return null;
            }
            comment.Text = commentDto.Text;
            comment.DateTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
