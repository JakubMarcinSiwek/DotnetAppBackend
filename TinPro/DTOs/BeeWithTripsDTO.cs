namespace TinPro.DTOs;

public class BeeWithTripsDTO
{
    public int Id_Bee { get; set; }
    
    public string Nickname { get; set; }
    
    public int Id_Role { get; set; }

    public string Role { get; set; }

    public ICollection<TripDTO2> TripDtos { get; set; }
}