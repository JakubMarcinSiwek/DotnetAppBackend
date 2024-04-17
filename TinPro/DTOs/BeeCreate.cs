using System.ComponentModel.DataAnnotations;

namespace TinPro.DTOs;
using TinPro.Models;
public class BeeCreate
{
    
    public int Id_Bee { get; set; }
    [Required(ErrorMessage = "Nickname must not be empty")]
    public string Nickname { get; set; }
    [Required(ErrorMessage = "Password must not be empty")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Id_Role must not be empty")]
    public int Id_Role { get; set; }
    
    
}