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
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }
        // ✅ ⿡ Lister tous les clients (utilisateurs)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetClients()
        {
            return await _context.Users.ToListAsync();
        }

        // ✅ ⿢ Récupérer un client par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetClient(int id)
        {
            var client = await _context.Users.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, [FromBody] User client)
        {
            if (client == null) return BadRequest("Le corps de la requête est vide.");
            if (id != client.Id) return BadRequest("L'ID fourni ne correspond pas à l'ID du client.");

            var existingClient = await _context.Users.FindAsync(id);
            if (existingClient == null) return NotFound("Client non trouvé.");

            existingClient.Nom = client.Nom;
            existingClient.Email = client.Email;
            existingClient.Telephone = client.Telephone;
            existingClient.Adresse = client.Adresse;
            existingClient.DateDeNaissance = client.DateDeNaissance;
            existingClient.Genre = client.Genre;
            existingClient.EstActif = client.EstActif;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // ✅ ⿥ Supprimer un client
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Users.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Users.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
