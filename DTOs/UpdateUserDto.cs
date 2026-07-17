using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.DTOs
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [MinLength(4, ErrorMessage = "Password must be at least 4 characters")]
        public string? Password { get; set; }
    }
}
