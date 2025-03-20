using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PFA.Models;
using PFA.Services;

namespace PFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly TransportService _transportService;

        public TransportController(TransportService transportService)
        {
            _transportService = transportService;
        }

        // 🔹 Récupérer tous les transports
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transports = await _transportService.GetAllTransportsAsync();
            return Ok(transports);
        }

        // 🔹 Récupérer un transport par ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transport = await _transportService.GetTransportByIdAsync(id);
            if (transport == null)
                return NotFound("Transport non trouvé");

            return Ok(transport);
        }

        // 🔹 Ajouter un transport
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Transport transport)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("⛔ Erreur de validation:", ModelState);
                return BadRequest(ModelState);
            }

            var newTransport = await _transportService.AddTransportAsync(transport);

            // ✅ Recharge l'entité pour inclure Destination
            var transportAvecDestination = await _transportService.GetTransportByIdAsync(newTransport.Id);

            return CreatedAtAction(nameof(GetById), new { id = transportAvecDestination.Id }, transportAvecDestination);
        }


        // 🔹 Mettre à jour un transport
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Transport transport)
        {
            if (id != transport.Id)
                return BadRequest("L'ID du transport ne correspond pas");

            var updated = await _transportService.UpdateTransportAsync(transport);
            if (!updated)
                return NotFound("Transport non trouvé");

            return NoContent();
        }

        // 🔹 Supprimer un transport
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _transportService.DeleteTransportAsync(id);
            if (!deleted)
                return NotFound("Transport non trouvé");

            return NoContent();
        }
    }
}
