using System.ComponentModel.DataAnnotations;

namespace StoreAPI.Application.DTOs;

public class LoginDto
{
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
    public required string Username { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
    public required string Password { get; set; }
}