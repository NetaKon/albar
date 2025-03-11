using System.ComponentModel.DataAnnotations;

namespace StoreAPI.Application.DTOs;

public class SignupDto
{
    [Required]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 30 characters.")]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(50, ErrorMessage = "Email must not exceed 50 characters.")]
    public required string Email { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
    public required string Password { get; set; }
}
