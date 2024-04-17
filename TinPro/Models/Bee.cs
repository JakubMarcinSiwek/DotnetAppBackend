namespace TinPro.Models;

public class Bee
{
    public int Id_Bee { get; set; }
    
    public string Nickname { get; set; }
    
    public string Password { get; set; }
    
    public int Id_Role { get; set; }
    public virtual Role Role { get; set; }
    public ICollection<Trip> Trips { get; set; }
}