namespace TinPro.Models;

public class Trip
{
    public int Id_Trip { get; set; }
    
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public string Objective { get; set; }
    
    public string Description { get; set; }
    public ICollection<Bee> Bees { get; set; }
    public ICollection<Place> Places { get; set; }
}