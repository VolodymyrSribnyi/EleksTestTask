using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public record StudentCreateUpdateDto
    {
        [Required(ErrorMessage = "The student's name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The name must contain between 2 and 50 characters")]
        public required string StudentFirstName { get; set; }
        [Required(ErrorMessage = "The student's surname is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The surname must contain between 2 and 50 characters")]
        public required string StudentLastName { get; set; }
    }
}
