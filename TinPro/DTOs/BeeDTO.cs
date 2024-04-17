using TinPro.Models;

namespace TinPro.DTOs;

public class BeeDTO
{
    public int Id_Bee { get; set; }
    
    public string Nickname { get; set; }
    
    public string Password { get; set; }
    
    public int Id_Role { get; set; }

    public string Role { get; set; }

}