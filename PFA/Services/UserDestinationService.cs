using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserDestinationService
{
    private readonly AppDbContext _context;

    public UserDestinationService(AppDbContext context)
    {
        _context = context;
    }

    // 📌 Récupérer les activités par destination
    public async Task<List<Activite>> GetActivitesParDestination(int destinationId)
    {
        return await _context.Activites
            .Where(a => a.DestinationId == destinationId)
            .ToListAsync();
    }

    // 📌 Récupérer les hébergements par destination
    public async Task<List<Hebergement>> GetHebergementsParDestination(int destinationId)
    {
        return await _context.Hebergements
            .Where(h => h.DestinationId == destinationId)
            .ToListAsync();
    }
}
