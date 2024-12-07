using System.ComponentModel.DataAnnotations;

namespace Cinematic.Dtos.CommentDtos
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(180)]
        public string Text { get; set; }
    }
}
