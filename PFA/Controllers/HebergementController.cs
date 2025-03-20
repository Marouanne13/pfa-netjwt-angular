using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;

[Route("api/hebergements")]
[ApiController]
public class HebergementController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly HebergementService _hebergementService;

    public HebergementController(AppDbContext context)
    {
        _context = context;
        _hebergementService = new HebergementService(_context); // 🔥 Initialisation directe ici
    }

    // Récupérer tous les hébergements
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Hebergement>>> GetAllHebergements()
    {
        var hebergements = await _hebergementService.GetAllHebergements();
        return Ok(hebergements);
    }

    // Récupérer par destination
    [HttpGet("destination/{id}")]
    public async Task<ActionResult<IEnumerable<Hebergement>>> GetHebergementsByDestination(int id)
    {
        var hebergements = await _context.Hebergements
            .Where(h => h.DestinationId == id)
            .ToListAsync();

        if (hebergements == null || hebergements.Count == 0)
        {
            return NotFound($"Aucun hébergement trouvé pour la destination {id}.");
        }

        return Ok(hebergements);
    }


    // Ajouter un hébergement
    [HttpPost("ajouter")]
    public async Task<ActionResult<Hebergement>> AjouterHebergement([FromBody] Hebergement hebergement)
    {
        if (hebergement == null)
            return BadRequest("Les données de l'hébergement sont invalides.");

        var destinationExiste = await _context.Destinations.FindAsync(hebergement.DestinationId);
        if (destinationExiste == null)
            return BadRequest($"La destination avec ID {hebergement.DestinationId} n'existe pas.");

        hebergement.Destination = null;
        _context.Hebergements.Add(hebergement);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAllHebergements), new { id = hebergement.Id }, hebergement);
    }

    // Modifier un hébergement
    [HttpPut("modifier/{id}")]
    public async Task<IActionResult> ModifierHebergement(int id, [FromBody] Hebergement hebergement)
    {
        if (id != hebergement.Id)
            return BadRequest("L'ID de l'hébergement ne correspond pas.");

        var hebergementExiste = await _context.Hebergements.FindAsync(id);
        if (hebergementExiste == null)
            return NotFound("Hébergement non trouvé.");

        _context.Entry(hebergementExiste).CurrentValues.SetValues(hebergement);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Supprimer un hébergement
    [HttpDelete("supprimer/{id}")]
    public async Task<IActionResult> SupprimerHebergement(int id)
    {
        var hebergement = await _context.Hebergements.FindAsync(id);
        if (hebergement == null)
            return NotFound("Hébergement non trouvé.");

        _context.Hebergements.Remove(hebergement);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
