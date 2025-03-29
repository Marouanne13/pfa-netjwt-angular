using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using PFA.Data;

namespace PFA.Controllers
{
    [Route("api/paiement")] // 👉 ce qui donne : /api/paiement/create-checkout-session/{userId}
    [ApiController]
    public class PaiementController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public PaiementController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;

            // ✅ Récupère la clé Stripe depuis appsettings.json
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        [HttpPost("create-checkout-session/{userId}")]
        public async Task<IActionResult> CreateCheckoutSession(int userId)
        {
            // 🔍 Récupère le dernier panier de l’utilisateur
            var panier = await _context.Panier
                .Include(p => p.Hebergement)
                .Include(p => p.Activite)
                .Include(p => p.Transport)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Id) // 🔁 prend le plus récent si plusieurs
                .FirstOrDefaultAsync();

            if (panier == null)
                return BadRequest("Aucun panier trouvé pour cet utilisateur.");

            double total = 0;

            if (panier.Hebergement != null)
                total += panier.Hebergement.PrixParNuit;

            if (panier.Activite != null)
                total += panier.Activite.Prix;

            if (panier.Transport != null)
                total += panier.Transport.Prix;

            // ✅ Configuration de la session Stripe Checkout
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(total * 100), // 💲 en centimes
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Paiement de votre panier",
                            },
                        },
                        Quantity = 1,
                    }
                },
                Mode = "payment",
                SuccessUrl = "http://localhost:4200/success",
                CancelUrl = "http://localhost:4200/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Ok(new { sessionId = session.Id });
        }
    }
}
