using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TinPro.DTOs;
using TinPro.Models;

namespace TinPro.Services;

public class DbService : IDbService
{
    private MYcontext _mYcontext;


    public DbService(MYcontext mYcontext)
    {
        _mYcontext = mYcontext;
    }

    public async Task<BeeDTO> getBee(int id)
    {
        var beeDto = _mYcontext.Bees.Where(p => p.Id_Bee == id).Select(p => new BeeDTO()
        {
            Id_Bee = p.Id_Bee, Nickname = p.Nickname, Password = p.Password, Id_Role = p.Id_Role, Role = p.Role.Name
        }).FirstOrDefault();

        if (beeDto == null) return null;

        return beeDto;
    }

    public async Task<ICollection<BeeDTO>> getAllBees(int pageNumber, int pageSize)
    {
        var bees = _mYcontext.Bees
            .OrderBy(bee => bee.Id_Bee)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .Select(p => new BeeDTO()
            {
                Id_Bee = p.Id_Bee, Nickname = p.Nickname, Password = p.Password, Id_Role = p.Id_Role, Role = p.Role.Name
            }).ToList();

        if (bees == null) return null;
        return bees;
    }

    public async Task<BeeWithTripsDTO> getBeeWithTrips(int beeId)
    {
        var beeWithTrips = await _mYcontext.Bees.Where(bee => bee.Id_Bee == beeId).Select(bee => new BeeWithTripsDTO
            {
                Id_Bee = bee.Id_Bee,
                Nickname = bee.Nickname,
                Id_Role = bee.Id_Role,
                Role = bee.Role.Name,
                TripDtos = bee.Trips
                    .Select(trip => new TripDTO2
                    {
                        Id_Trip = trip.Id_Trip,
                        StartDate = trip.StartDate,
                        EndDate = trip.EndDate,
                        Description = trip.Description,
                        Objective = trip.Objective,
                        Places = trip.Places
                            .Select(place => new PlaceSmallDTO
                            {
                                Name = place.Name,
                                Location = place.Location
                            })
                            .ToList()
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        return beeWithTrips;
    }

    public Task<string> updateBee(int id, BeeCreate bee)
    {
        throw new NotImplementedException();
    }


    public async Task<string> insertBee(BeeCreate bee)
    {
        if (await _mYcontext.Bees.AnyAsync(b => b.Id_Bee == bee.Id_Bee))
        {
            return "Bee with the specified ID already exists";
        }

        var passwordHasher = new PasswordHasher<string>();
        var hashed = passwordHasher.HashPassword(null, bee.Password);

        var beeToAdd = new Bee()
        {
            Id_Bee = bee.Id_Bee,
            Nickname = bee.Nickname,
            Password = hashed,
            Id_Role = bee.Id_Role
        };

        _mYcontext.Bees.Add(beeToAdd);

        try
        {
            await _mYcontext.SaveChangesAsync();
            return "Bee added successfully";
        }
        catch (Exception ex)
        {
            return $"Error adding bee: {ex.Message}";
        }
    }

    public async Task<string> deleteBee(int id)
    {
        var beeToRemove = await _mYcontext.Bees.FirstOrDefaultAsync(b => b.Id_Bee == id);
        if (beeToRemove == null) return "No bee to remove";
        _mYcontext.Bees.Remove(_mYcontext.Bees.First(b => b.Id_Bee == id));
        await _mYcontext.SaveChangesAsync();
        return "bee removed";
    }

    public async Task<string> addBeeToTrip(int beeId, int tripId)
    {
        var bee = await _mYcontext.Bees.Include(bee1 => bee1.Trips).FirstOrDefaultAsync(b => b.Id_Bee == beeId);
        var trip = await _mYcontext.Trips.FirstOrDefaultAsync(b => b.Id_Trip == tripId);
        bee.Trips.Add(trip);
        await _mYcontext.SaveChangesAsync();
        return "bee added to trip";
    }

    public async Task<TripDTO> getTrip(int id)
    {
        var bees = await _mYcontext.Bees.Where(bee => bee.Trips.Any(trip => trip.Id_Trip == id))
            .Select(bee => new BeeSmallDTO() { Nickname = bee.Nickname }).ToListAsync();
        var places = await _mYcontext.Places.Where(place => place.Trips.Any(trip => trip.Id_Trip == id))
            .Select(place => new PlaceSmallDTO() { Name = place.Name, Location = place.Location }).ToListAsync();
        var tripShow = await _mYcontext.Trips.FirstOrDefaultAsync(trip1 => trip1.Id_Trip == id);
        var trip = new TripDTO()
        {
            StartDate = tripShow.StartDate,
            EndDate = tripShow.EndDate,
            Bees = bees,
            Description = tripShow.Description,
            Objective = tripShow.Objective,
            Places = places
        };
        return trip;
    }

    public async Task<string> CreateTrip(TripCreateDTO tripDto)
    {
        var tripEntity = new Trip
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(7),
            Objective = tripDto.Objective,
            Description = tripDto.Description
        };

        _mYcontext.Trips.Add(tripEntity);
        await _mYcontext.SaveChangesAsync();

        return "Trip created successfully";
    }


    public async Task<string> addPlace(PlaceDTO place)
    {
        var placeToadd = new Place()
        {
            Id_Place = place.Id_Place,
            Name = place.Name,
            Location = place.Location
        };

        _mYcontext.Places.Add(placeToadd);
        try
        {
            await _mYcontext.SaveChangesAsync();
            return "Place added successfully";
        }
        catch (Exception ex)
        {
            return $"Error adding place: {ex.Message}";
        }
    }

    public async Task<string> removePlace(int id)
    {
        var placeToRemove = await _mYcontext.Places.FirstOrDefaultAsync(place1 => place1.Id_Place == id);

        if (placeToRemove == null)
        {
            return "Place not found";
        }

        _mYcontext.Places.Remove(placeToRemove);

        try
        {
            await _mYcontext.SaveChangesAsync();
            return "Place removed successfully";
        }
        catch (Exception ex)
        {
            return $"Error removing place: {ex.Message}";
        }
    }

    public async Task<string> assignPlace(int idPlace, int idTrip)
    {
        var place = await _mYcontext.Places.FirstOrDefaultAsync(p => p.Id_Place == idPlace);
        var trip = await _mYcontext.Trips.Include(t => t.Places).FirstOrDefaultAsync(t => t.Id_Trip == idTrip);

        if (place == null || trip == null)
        {
            return "Place or Trip not found";
        }

        trip.Places.Add(place);

        await _mYcontext.SaveChangesAsync();
        return "Place assigned to Trip successfully";
    }

    public async Task<Bee?> Authenticate(LoginDTO loginDto)
    {
        var bee = await _mYcontext.Bees.Include(bee1 => bee1.Role).FirstOrDefaultAsync(bee => bee.Nickname == loginDto.Nickname);
        var passwordHasher = new PasswordHasher<string>();
        var verification = passwordHasher.VerifyHashedPassword(null, bee.Password, loginDto.Password);
        if (verification==PasswordVerificationResult.Success)
        {
            return bee;
        }

        return null;
    }

    public async Task<string> GenerateToken(Bee bee)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key =
            "lorem ipsum mimeimiememakjnelgkjalkjnaksjglpppgksalknglknmsa glkmawlkmgalwkjnglkjawklp;jgkoawjkowjgploajkwkgjkjwglakjakwjgl"u8
                .ToArray();
        string? role = await _mYcontext.Roles.Where(role1 => role1.Id_Role==bee.Id_Role).Select(role1 => role1.Name).FirstOrDefaultAsync() ;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, bee.Id_Bee.ToString()), new Claim(ClaimTypes.Name, bee.Nickname),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddYears(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}