namespace TinPro.DTOs;

public class TripDTO
{
    
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public string Objective { get; set; }
    
    public string Description { get; set; }
    public ICollection<BeeSmallDTO> Bees { get; set; }
    public ICollection<PlaceSmallDTO> Places { get; set; }
}