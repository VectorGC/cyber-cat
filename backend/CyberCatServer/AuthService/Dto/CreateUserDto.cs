using System.ComponentModel.DataAnnotations;

namespace AuthService.Dto;

public class CreateUserDto
{
    [Required] public string Name { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }

    [Required] public string Password { get; set; }
}