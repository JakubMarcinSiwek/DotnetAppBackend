namespace TinPro.DTOs;

public class TripDTO2
{
    public int Id_Trip { get; set; }
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public string Objective { get; set; }
    
    public string Description { get; set; }
    public ICollection<PlaceSmallDTO> Places { get; set; }
}