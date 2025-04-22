using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using PFA.Data;
using PFA.Models;

[Route("api/[controller]")]
[ApiController]
// ❌ Retirer l'attribut [Authorize] pour permettre l'accès sans authentification
public class RestaurantsUserController : ControllerBase
{
    private readonly AppDbContext _context;

    public RestaurantsUserController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/restaurantsUser
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
    {
        var restaurants = await _context.Restaurant.ToListAsync();
        return Ok(restaurants);
    }

    // Optionnel : recherche simple
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Restaurant>>> SearchRestaurants([FromQuery] string query)
    {
        var results = await _context.Restaurant
            .Where(r => r.Nom.Contains(query) || r.Adresse.Contains(query))
            .ToListAsync();
        return Ok(results);
    }
}
