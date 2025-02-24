using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PFA.Services
{
    public class ActiviteService
    {
        private readonly AppDbContext _context;

        public ActiviteService(AppDbContext context)
        {
            _context = context;
        }

        // Récupérer toutes les activités
        public async Task<IEnumerable<Activite>> GetAllAsync()
        {
            return await _context.Activites.Include(a => a.Destination).ToListAsync();
        }

        // Récupérer une activité par ID
        public async Task<Activite> GetByIdAsync(int id)
        {
            return await _context.Activites.Include(a => a.Destination)
                                           .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Ajouter une activité
        public async Task<Activite> AddAsync(Activite activite)
        {
            activite.Destination = null; // Ne pas exiger l'objet Destination
            _context.Activites.Add(activite);
            await _context.SaveChangesAsync();
            return activite;
        }


        // Modifier une activité
        public async Task<Activite> UpdateAsync(Activite activite)
        {
            _context.Activites.Update(activite);
            await _context.SaveChangesAsync();
            return activite;
        }

        // Supprimer une activité
        public async Task<bool> DeleteAsync(int id)
        {
            var activite = await _context.Activites.FindAsync(id);
            if (activite == null) return false;

            _context.Activites.Remove(activite);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
