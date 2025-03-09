using Microsoft.AspNetCore.Mvc;
using PFA.Models;
using PFA.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PFA.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessagesController(MessageService messageService)
        {
            _messageService = messageService;
        }

        // 🔥 Envoyer un message
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            if (message == null || string.IsNullOrWhiteSpace(message.Contenu))
                return BadRequest("Le message ne peut pas être vide.");

            var sentMessage = await _messageService.SendMessage(message);
            if (sentMessage == null)
                return BadRequest("Erreur lors de l'envoi du message.");

            // ✅ Correction : Renommé `ExpéditeurId` → `ExpediteurId` sans accent
            return CreatedAtAction(nameof(GetConversation), new { userId = sentMessage.ExpediteurId, adminId = sentMessage.DestinataireId }, sentMessage);
        }

        // 🔥 Récupérer les messages entre un utilisateur et un administrateur
        [HttpGet("conversation")]
        public async Task<IActionResult> GetConversation([FromQuery] int userId, [FromQuery] int adminId)
        {
            if (userId <= 0 || adminId <= 0)
                return BadRequest("Les identifiants doivent être valides.");

            var messages = await _messageService.GetMessagesBetweenUsers(userId, adminId);
            if (messages == null || messages.Count == 0)
                return NotFound("Aucun message trouvé entre ces utilisateurs.");

            return Ok(messages);
        }

        // 🔥 Marquer un message comme lu
        [HttpPut("mark-as-read/{messageId}")]
        public async Task<IActionResult> MarkMessageAsRead(int messageId)
        {
            if (messageId <= 0)
                return BadRequest("ID de message invalide.");

            var updated = await _messageService.MarkMessageAsRead(messageId);
            if (!updated) return NotFound("Message non trouvé.");

            return NoContent();
        }

        // 🔥 Identifier si l'expéditeur est un User ou un Admin
        [HttpGet("expediteur/{id}")]
        public async Task<IActionResult> IdentifierExpediteur(int id)
        {
            if (id <= 0)
                return BadRequest("ID invalide.");

            // ✅ Correction : Renommé `IdentifierExpéditeur` → `IdentifierExpediteur`
            var type = await _messageService.IdentifierExpediteur(id);
            if (string.IsNullOrEmpty(type))
                return NotFound("Utilisateur non trouvé.");

            return Ok(new { ExpediteurType = type });
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetMessagesForUser(int userId)
        {
            if (userId <= 0)
                return BadRequest("ID utilisateur invalide.");

            var messages = await _messageService.GetMessagesForUser(userId);
            if (messages == null || messages.Count == 0)
                return NotFound("Aucun message trouvé pour cet utilisateur.");

            return Ok(messages);
        }
        [HttpGet("admin/messages")]
        public async Task<IActionResult> GetAllMessagesForAdmin()
        {
            var messages = await _messageService.GetAllMessages();
            if (messages == null || messages.Count == 0)
                return NotFound("Aucun message trouvé.");

            return Ok(messages);
        }

    }
}
