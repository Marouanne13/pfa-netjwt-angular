using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DestinationsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 📌 Récupérer toutes les destinations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destination>>> GetDestinations()
        {
            return await _context.Destinations.ToListAsync();
        }

        // ✅ 📌 Récupérer une destination par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Destination>> GetDestination(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            return destination;
        }

        // ✅ 📌 Ajouter une nouvelle destination
        [HttpPost]
        public async Task<ActionResult<Destination>> PostDestination(Destination destination)
        {
            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDestination), new { id = destination.Id }, destination);
        }

        // ✅ 📌 Modifier une destination
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestination(int id, Destination destination)
        {
            if (id != destination.Id)
            {
                return BadRequest();
            }

            _context.Entry(destination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Destinations.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // ✅ 📌 Supprimer une destination
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null)
            {
                return NotFound();
            }

            _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
