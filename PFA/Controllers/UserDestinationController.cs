using Microsoft.AspNetCore.Mvc;
using PFA.Services;
using System.Threading.Tasks;

[Route("api/user-destination")]
[ApiController]
public class UserDestinationController : ControllerBase
{
    private readonly UserDestinationService _userDestinationService;

    public UserDestinationController(UserDestinationService userDestinationService)
    {
        _userDestinationService = userDestinationService;
    }

    // 📌 Récupérer les activités d'une destination
    [HttpGet("activites/{destinationId}")]
    public async Task<IActionResult> GetActivitesParDestination(int destinationId)
    {
        var activites = await _userDestinationService.GetActivitesParDestination(destinationId);
        if (activites == null || activites.Count == 0)
            return NotFound("Aucune activité trouvée pour cette destination.");

        return Ok(activites);
    }

    // 📌 Récupérer les hébergements d'une destination
    [HttpGet("hebergements/{destinationId}")]
    public async Task<IActionResult> GetHebergementsParDestination(int destinationId)
    {
        var hebergements = await _userDestinationService.GetHebergementsParDestination(destinationId);
        if (hebergements == null || hebergements.Count == 0)
            return NotFound("Aucun hébergement trouvé pour cette destination.");

        return Ok(hebergements);
    }
}
