namespace TinPro.Models;

public class Role
{
    
    public int Id_Role { get; set; }

    
    public string Name { get; set; }

    public ICollection<Bee> Bees { get; set; }
}