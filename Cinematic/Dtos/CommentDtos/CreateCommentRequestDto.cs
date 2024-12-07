using Cinematic.Models;
using System.ComponentModel.DataAnnotations;

namespace Cinematic.Dtos.CommentDtos
{
    public class CreateCommentRequestDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(180)]
        public string Text { get; set; }
        [Required]
        public int MovieId { get; set; }
    }
}
