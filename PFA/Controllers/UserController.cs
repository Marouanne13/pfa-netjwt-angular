using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PFA.Data;
using PFA.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace PFA.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ✅ INSCRIPTION
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
        {
            // 📌 Vérification des champs obligatoires
            if (string.IsNullOrWhiteSpace(model.Nom) || string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.Telephone) ||
                string.IsNullOrWhiteSpace(model.Adresse) || string.IsNullOrWhiteSpace(model.Genre) ||
                model.DateDeNaissance == default)
            {
                return BadRequest("Tous les champs sont obligatoires.");
            }

            // 📌 Vérification de l'email
            if (!model.Email.Contains("@"))
                return BadRequest("Email invalide.");

            // 📌 Vérifier si l'email existe déjà
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return BadRequest("Cet email est déjà utilisé.");

            // 📌 Vérification de la longueur du mot de passe
            if (model.Password.Length < 6)
                return BadRequest("Le mot de passe doit contenir au moins 6 caractères.");

            // 📌 Hachage du mot de passe
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, workFactor: 12);

            var user = new User
            {
                Nom = model.Nom,
                Email = model.Email,
                MotDePasse = hashedPassword,
                Telephone = model.Telephone,
                Adresse = model.Adresse,
                DateDeNaissance = model.DateDeNaissance,
                Genre = model.Genre,
                DateDeCreation = DateTime.UtcNow,
                EstActif = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Utilisateur enregistré avec succès" });
        }

        // ✅ CONNEXION
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest("L'email et le mot de passe sont obligatoires.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
                return Unauthorized("Email ou mot de passe incorrect.");

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.MotDePasse))
                return Unauthorized("Email ou mot de passe incorrect.");

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                Token = token,
                UserId = user.Id,
                Email = user.Email
            });
        }

        // ✅ GÉNÉRATION DU TOKEN JWT
        private string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "CeciEstUneSuperCleSecreteJWT123456789");
            var issuer = _configuration["Jwt:Issuer"] ?? "https://localhost:5278";
            var audience = _configuration["Jwt:Audience"] ?? "https://localhost:5278";

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //    new Claim(ClaimTypes.Email, user.Email),
            //    new Claim(ClaimTypes.Role, "User")
            //};

            var claims = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("role", "User")
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

    // ✅ Modèle pour l'inscription (tous les champs obligatoires)
    public class RegisterUserModel
    {
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public string Genre { get; set; }
    }

    // ✅ Modèle pour la connexion (champs obligatoires)
    public class LoginUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
