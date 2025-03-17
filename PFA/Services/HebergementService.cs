using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class HebergementService
{
    private readonly AppDbContext _context;

    public HebergementService(AppDbContext context)
    {
        _context = context;
    }

    // ✅ 📌 Récupérer tous les hébergements
    public async Task<List<Hebergement>> GetAllHebergements()
    {
        return await _context.Hebergements.AsNoTracking().ToListAsync();
    }

    // ✅ 📌 Récupérer un hébergement par ID
    public async Task<Hebergement?> GetHebergementById(int id)
    {
        return await _context.Hebergements
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    // ✅ 📌 Récupérer les hébergements d'une destination spécifique
    public async Task<List<Hebergement>> GetHebergementsParDestination(int destinationId)
    {
        return await _context.Hebergements
            .AsNoTracking()
            .Where(h => h.DestinationId == destinationId)
            .ToListAsync();
    }

    // ✅ 📌 Ajouter un hébergement
    public async Task<Hebergement> AjouterHebergement(Hebergement hebergement)
    {
        // Vérifier si la destination existe avant d'ajouter l'hébergement
        var destinationExiste = await _context.Destinations.FindAsync(hebergement.DestinationId);
        if (destinationExiste == null)
            throw new Exception($"La destination avec ID {hebergement.DestinationId} n'existe pas.");

        _context.Hebergements.Add(hebergement);
        await _context.SaveChangesAsync();
        return hebergement;
    }

    // ✅ 📌 Modifier un hébergement
    public async Task<bool> ModifierHebergement(int id, Hebergement hebergement)
    {
        var existing = await _context.Hebergements.FindAsync(id);
        if (existing == null)
            return false;

        _context.Entry(existing).CurrentValues.SetValues(hebergement);
        await _context.SaveChangesAsync();
        return true;
    }

    // ✅ 📌 Supprimer un hébergement
    public async Task<bool> SupprimerHebergement(int id)
    {
        var hebergement = await _context.Hebergements.FindAsync(id);
        if (hebergement == null)
            return false;

        _context.Hebergements.Remove(hebergement);
        await _context.SaveChangesAsync();
        return true;
    }
}
