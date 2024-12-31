using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PFA.Services
{
    public class DestinationService
    {
        private readonly AppDbContext _context;

        public DestinationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Destination>> GetAllDestinationsAsync()
        {
            return await _context.Destinations.ToListAsync();
        }

        public async Task<IEnumerable<Destination>> GetDestinationsByRegionAsync(string region)
        {
            return await _context.Destinations.Where(d => d.Region == region).ToListAsync();
        }

        public async Task<IEnumerable<Destination>> GetPopularDestinationsAsync()
        {
            return await _context.Destinations.Where(d => d.EstPopulaire).ToListAsync();
        }

        public async Task CreateDestinationAsync(Destination destination)
        {
            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDestinationAsync(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination != null)
            {
                _context.Destinations.Remove(destination);
                await _context.SaveChangesAsync();
            }
        }
    }
}
