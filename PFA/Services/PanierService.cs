using System.Linq;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;

public class PanierService
{
    private readonly AppDbContext _context;

    public PanierService(AppDbContext context)
    {
        _context = context;
    }

    public Panier GetPanierByUserId(int userId)
    {
        return _context.Paniers
            .Include(p => p.Destination)
            .Include(p => p.Hebergements)
            .Include(p => p.Activite)
            .Include(p => p.Transport)
            .Include(p => p.Restaurant)
            .FirstOrDefault(p => p.UserId == userId);
    }

    public void AjouterAuPanier(int userId, int? destinationId, int? hebergementId, int? activiteId, int? transportId, int? restaurantId)
    {
        var panier = _context.Paniers.FirstOrDefault(p => p.UserId == userId);
        if (panier == null)
        {
            panier = new Panier { UserId = userId };
            _context.Paniers.Add(panier);
        }

        if (destinationId.HasValue) panier.DestinationId = destinationId.Value;
        if (hebergementId.HasValue) panier.HebergementId = hebergementId.Value;
        if (activiteId.HasValue) panier.ActiviteId = activiteId.Value;
        if (transportId.HasValue) panier.TransportId = transportId.Value;
        if (restaurantId.HasValue) panier.RestaurantId = restaurantId.Value;

        _context.SaveChanges();
    }

    public void SupprimerDuPanier(int userId)
    {
        var panier = _context.Paniers.FirstOrDefault(p => p.UserId == userId);
        if (panier != null)
        {
            _context.Paniers.Remove(panier);
            _context.SaveChanges();
        }
    }

    public void ViderPanier(int userId)
    {
        var panier = _context.Paniers.FirstOrDefault(p => p.UserId == userId);
        if (panier != null)
        {
            panier.DestinationId = null;
            panier.HebergementId = null;
            panier.ActiviteId = null;
            panier.TransportId = null;
            panier.RestaurantId = null;
            _context.SaveChanges();
        }
    }
}
