using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;

namespace PFA.Services
{
    public class TransportService
    {
        private readonly AppDbContext _context;

        public TransportService(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 Récupérer tous les transports
        public async Task<List<Transport>> GetAllTransportsAsync()
        {
            return await _context.Transports
                .Include(t => t.Destination) // Jointure avec Destination
                .ToListAsync();
        }

        // 🔹 Récupérer un transport par ID
        public async Task<Transport?> GetTransportByIdAsync(int id)
        {
            return await _context.Transports
                .Include(t => t.Destination)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        // 🔹 Ajouter un transport
        public async Task<Transport> AddTransportAsync(Transport transport)
        {
            _context.Transports.Add(transport);
            await _context.SaveChangesAsync();
            return transport;
        }

        // 🔹 Mettre à jour un transport
        public async Task<bool> UpdateTransportAsync(Transport transport)
        {
            var existingTransport = await _context.Transports.FindAsync(transport.Id);
            if (existingTransport == null) return false;

            existingTransport.Capacite = transport.Capacite;
            existingTransport.EstDisponible = transport.EstDisponible;
            existingTransport.TypeTransport = transport.TypeTransport;
            existingTransport.Prix = transport.Prix;
            existingTransport.NomCompagnie = transport.NomCompagnie;
            existingTransport.ModeDeTransport = transport.ModeDeTransport;
            existingTransport.NumeroImmatriculation = transport.NumeroImmatriculation;
            existingTransport.HeureDepart = transport.HeureDepart;
            existingTransport.HeureArrivee = transport.HeureArrivee;
            existingTransport.NumeroServiceClient = transport.NumeroServiceClient;
            existingTransport.DestinationId = transport.DestinationId;

            await _context.SaveChangesAsync();
            return true;
        }

        // 🔹 Supprimer un transport
        public async Task<bool> DeleteTransportAsync(int id)
        {
            var transport = await _context.Transports.FindAsync(id);
            if (transport == null) return false;

            _context.Transports.Remove(transport);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
