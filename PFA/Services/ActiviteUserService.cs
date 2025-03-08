using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFA.Services
{
    public class ActiviteUserService
    {
        private readonly AppDbContext _context;

        public ActiviteUserService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 📌 Récupérer toutes les activités disponibles
        public async Task<IEnumerable<Activite>> GetAllActivites()
        {
            return await _context.Activites
                .Where(a => a.EstDisponible) // Filtrer les activités disponibles
                .ToListAsync();
        }

        // ✅ 📌 Récupérer une activité par ID
        public async Task<Activite?> GetActiviteById(int id)
        {
            return await _context.Activites
                .Where(a => a.EstDisponible) // Vérifier si l'activité est disponible
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // ✅ 📌 Récupérer les activités d'une destination spécifique
        public async Task<IEnumerable<Activite>> GetActivitesByDestination(int destinationId)
        {
            return await _context.Activites
                .Where(a => a.DestinationId == destinationId && a.EstDisponible) // Vérifier la destination et la disponibilité
                .ToListAsync();
        }
    }
}
