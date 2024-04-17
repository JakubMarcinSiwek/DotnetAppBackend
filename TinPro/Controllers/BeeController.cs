using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinPro.DTOs;
using TinPro.Services;

namespace TinPro.Controllers;
[ApiController]
[Authorize(Roles = "Worker Bee, Queen, Army Bee")]
[Route("[controller]")]
public class BeeController : Controller
{

    private IDbService _dbService;

    public BeeController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("api/showBees")]
    public async Task<IActionResult> showBees(int pageNumber,int pageSize)
    {
        var bees = await _dbService.getAllBees( pageNumber, pageSize);
        if (bees == null) return NotFound("no bees");
        return Ok(bees);
    }

    [HttpGet("api/showBee/{id}")]
    public async Task<IActionResult> showBee(int id)
    {
        var bee = await _dbService.getBee(id);
        if (bee != null)
        {
            return Ok(bee);
        }

        return NotFound("No such bee");
    }

    [HttpGet("api/showBeeWithTrips/{id}")]
    public async Task<IActionResult> showBeeWithTrips(int id)
    {
        var bee = await _dbService.getBeeWithTrips(id);
        if (bee != null)
        {
            return Ok(bee);
        }

        return NotFound("No such bee");
    }
    

    [HttpDelete("api/deleteBee/{id}")]
    public async Task<IActionResult> deleteBee(int id)
    {
        string res = "";
        res = await _dbService.deleteBee(id);
        return Ok(res);
    }

    [HttpPost("api/createBee")]
    public async Task<IActionResult> createBee(BeeCreate beeCreate)
    {
        string res = "";
        res = await _dbService.insertBee(beeCreate);
        if(res.Equals("Bee added successfully"))return Ok(res);
        if (res.Equals("Bee with the specified ID already exists")) return NotFound(res);
        return BadRequest(res);

    }

    [HttpPost("api/addBeeToTrip")]
    public async Task<IActionResult> addBeeToTrip(int idb, int idt)
    {
        string res = "";
        res = await _dbService.addBeeToTrip(idb, idt);
        return Ok(res);
    }

    [HttpGet("api/getTrip/{id}")]
    public async Task<IActionResult> getTrip(int id)
    {
        var trips = await _dbService.getTrip(id);
        return Ok(trips);
    }
    [HttpPost("api/createTrip")]
    public async Task<IActionResult> CreateTrip([FromBody] TripCreateDTO tripDto)
    {
        string result = await _dbService.CreateTrip(tripDto);
        return Ok(result);
    }

    [HttpPost("api/addPlace")]
    public async Task<IActionResult> addPlace(PlaceDTO placeDto)
    {
       var res= await _dbService.addPlace(placeDto);
        return Ok(res);
    }

    [HttpDelete("api/deletePlace/{id}")]
    public async Task<IActionResult> deletePlace(int id)
    {
        var res = await _dbService.removePlace(id);
        return Ok(res);
    }
    [HttpPost("api/assignPlace")]
    public async Task<IActionResult> assignTrip(int idP,int idT)
    {
        var res = await _dbService.assignPlace(idP, idT);
        return Ok(res);
    }

}