using Microsoft.AspNetCore.Mvc;
using PFA.Models;
using PFA.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class DestinationController : ControllerBase
{
    private readonly DestinationService _destinationService;

    public DestinationController(DestinationService destinationService)
    {
        _destinationService = destinationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Destination>>> GetAllDestinations()
    {
        var destinations = await _destinationService.GetAllDestinationsAsync();
        return Ok(destinations);
    }

    [HttpGet("region/{region}")]
    public async Task<ActionResult<IEnumerable<Destination>>> GetDestinationsByRegion(string region)
    {
        var destinations = await _destinationService.GetDestinationsByRegionAsync(region);
        return Ok(destinations);
    }

    [HttpGet("popular")]
    public async Task<ActionResult<IEnumerable<Destination>>> GetPopularDestinations()
    {
        var destinations = await _destinationService.GetPopularDestinationsAsync();
        return Ok(destinations);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDestination(Destination destination)
    {
        await _destinationService.CreateDestinationAsync(destination);
        return CreatedAtAction(nameof(GetAllDestinations), new { id = destination.Id }, destination);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDestination(int id)
    {
        await _destinationService.DeleteDestinationAsync(id);
        return NoContent();
    }
}
