using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PFA.Data;
using PFA.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace PFA.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // 📌 ✅ INSCRIPTION (REGISTER)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // 🔹 Vérifier si tous les champs sont remplis
            if (string.IsNullOrWhiteSpace(model.Nom) || string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.Role))
            {
                return BadRequest("Tous les champs sont obligatoires.");
            }

            // 🔹 Vérifier si l'email est valide
            if (!model.Email.Contains("@"))
                return BadRequest("Email invalide.");

            // 🔹 Vérifier si l'email existe déjà
            if (await _context.Admins.AnyAsync(a => a.Email == model.Email))
                return BadRequest("Cet email est déjà utilisé.");

            // 🔹 Vérifier la longueur du mot de passe
            if (model.Password.Length < 6)
                return BadRequest("Le mot de passe doit contenir au moins 6 caractères.");

            // 🔹 Hacher le mot de passe avec BCrypt (12 rounds)
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, workFactor: 12);

            var admin = new Admin
            {
                Nom = model.Nom,
                Email = model.Email,
                MotDePasse = hashedPassword,
                Role = model.Role,
                DateDeCreation = DateTime.UtcNow
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Admin enregistré avec succès" });
        }

        // 📌 ✅ CONNEXION (LOGIN)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Vérifier si l'email existe en base
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == model.Email);
            if (admin == null)
                return Unauthorized("Email ou mot de passe incorrect.");

            // Vérifier si le mot de passe stocké est bien haché (sinon, c'est un ancien compte avec un mot de passe en clair)
            if (!admin.MotDePasse.StartsWith("$2"))
            {
                return BadRequest("Le mot de passe en base de données n'est pas sécurisé. Veuillez le réinitialiser.");
            }

            // Vérifier si le mot de passe entré correspond au mot de passe haché
            if (!BCrypt.Net.BCrypt.Verify(model.Password, admin.MotDePasse))
                return Unauthorized("Email ou mot de passe incorrect.");

            // Générer le token JWT
            var token = GenerateJwtToken(admin);

            // 🔹 Log du token pour vérifier son contenu
            Console.WriteLine($"Token généré: {token}");

            return Ok(new
            {
                Token = token,
                Role = admin.Role // 🔹 Ajout du rôle dans la réponse pour Angular
            });
        }

        // 📌 ✅ GÉNÉRATION DU JWT TOKEN
        private string GenerateJwtToken(Admin admin)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "CeciEstUneSuperCleSecreteJWT123456789");
            var issuer = _configuration["Jwt:Issuer"] ?? "https://localhost:5278";
            var audience = _configuration["Jwt:Audience"] ?? "https://localhost:5278";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim(ClaimTypes.Role, admin.Role), // ✅ Ajout du rôle
                new Claim("role", admin.Role) // ✅ Ajout du rôle sous un autre format
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // 📌 ✅ Modèle pour l'inscription
    public class RegisterModel
    {
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    // 📌 ✅ Modèle pour la connexion
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
