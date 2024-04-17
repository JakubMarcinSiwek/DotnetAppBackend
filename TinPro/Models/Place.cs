namespace TinPro.Models;

public class Place
{
    
    public int Id_Place { get; set; }
    
    public string Name { get; set; }
    
    public string Location { get; set; }

    public ICollection<Trip> Trips { get; set; }
    
}