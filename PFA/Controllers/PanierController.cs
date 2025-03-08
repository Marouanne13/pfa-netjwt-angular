using Microsoft.AspNetCore.Mvc;
using PFA.Services;
using PFA.Models;

[Route("api/panier")]
[ApiController]
public class PanierController : ControllerBase
{
    private readonly PanierService _panierService;

    public PanierController(PanierService panierService)
    {
        _panierService = panierService;
    }

    [HttpGet("{userId}")]
    public IActionResult GetPanier(int userId)
    {
        var panier = _panierService.GetPanierByUserId(userId);
        if (panier == null)
        {
            return NotFound("Panier non trouvé.");
        }
        return Ok(panier);
    }

    [HttpPost("ajouter")]
    public IActionResult AjouterAuPanier([FromBody] PanierRequest request)
    {
        _panierService.AjouterAuPanier(request.UserId, request.DestinationId, request.HebergementId, request.ActiviteId, request.TransportId, request.RestaurantId);
        return Ok("Ajouté au panier avec succès.");
    }

    [HttpDelete("supprimer/{userId}")]
    public IActionResult SupprimerPanier(int userId)
    {
        _panierService.SupprimerDuPanier(userId);
        return Ok("Panier supprimé.");
    }

    [HttpPut("vider/{userId}")]
    public IActionResult ViderPanier(int userId)
    {
        _panierService.ViderPanier(userId);
        return Ok("Panier vidé.");
    }
}

public class PanierRequest
{
    public int UserId { get; set; }
    public int? DestinationId { get; set; }
    public int? HebergementId { get; set; }
    public int? ActiviteId { get; set; }
    public int? TransportId { get; set; }
    public int? RestaurantId { get; set; }
}
