using System.ComponentModel.DataAnnotations;

namespace TinPro.DTOs;

public class LoginDTO
{
    [Required(ErrorMessage = "Nickname must not be empty")]
    public string Nickname { get; set; }
    [Required(ErrorMessage = "Nickname must not be empty")]
    public string Password { get; set; }
}