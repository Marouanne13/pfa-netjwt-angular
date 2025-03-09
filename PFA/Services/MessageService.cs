using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFA.Services
{
    public class MessageService
    {
        private readonly AppDbContext _context;

        public MessageService(AppDbContext context)
        {
            _context = context;
        }

        // Envoyer un message
        public async Task<Message> SendMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        // Récupérer les messages entre un utilisateur et un administrateur
        public async Task<List<Message>> GetMessagesBetweenUsers(int userId, int adminId)
        {
            return await _context.Messages
                .Where(m => (m.ExpediteurId == userId && m.DestinataireId == adminId) ||
                            (m.ExpediteurId == adminId && m.DestinataireId == userId))
                .OrderBy(m => m.DateEnvoi)
                .ToListAsync();
        }

        // Identifier si l'expéditeur est un User ou un Admin
        public async Task<string> IdentifierExpéditeur(int expéditeurId)
        {
            var utilisateur = await _context.Users.FindAsync(expéditeurId);
            if (utilisateur != null) return "User";

            var admin = await _context.Admins.FindAsync(expéditeurId);
            if (admin != null) return "Admin";

            return "Inconnu";
        }

        // Marquer un message comme lu
        public async Task<bool> MarkMessageAsRead(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message == null) return false;

            message.EstLu = true;
            _context.Messages.Update(message);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<string> IdentifierExpediteur(int id)
        {
            var message = await _context.Messages
                .Where(m => m.ExpediteurId == id)
                .FirstOrDefaultAsync();

            if (message == null)
                return string.Empty; // Aucun message trouvé

            return message.ExpediteurType; // Retourne "User" ou "Admin"
        }
        public async Task<List<Message>> GetMessagesForUser(int userId)
        {
            return await _context.Messages
                .Where(m => m.DestinataireId == userId || m.ExpediteurId == userId)
                .OrderBy(m => m.DateEnvoi)
                .ToListAsync();
        }
        public async Task<List<Message>> GetAllMessages()
        {
            return await _context.Messages
                .OrderByDescending(m => m.DateEnvoi)
                .ToListAsync();
        }

    }
}
