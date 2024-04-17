using TinPro.DTOs;
using TinPro.Models;

namespace TinPro.Services;

public interface IDbService
{
    public Task<BeeDTO> getBee(int id);
    public Task<ICollection<BeeDTO>> getAllBees(int pageNumber,int pageSize);

    public Task<BeeWithTripsDTO> getBeeWithTrips(int id);

    public Task<string> updateBee(int id, BeeCreate bee);

    public Task<string> insertBee(BeeCreate bee);

    public Task<string> deleteBee(int id);

    public Task<string> addBeeToTrip(int beeId, int tripId);

    public Task<TripDTO> getTrip(int id);
    
    public Task<string> CreateTrip(TripCreateDTO tripDto);

    public Task<string> addPlace(PlaceDTO place);
    
    public Task<string> removePlace(int id);

    public Task<string> assignPlace(int idPlace, int idTrip);
    
    public Task<Bee> Authenticate(LoginDTO loginDto);
    
    public Task<string> GenerateToken(Bee bee);
}