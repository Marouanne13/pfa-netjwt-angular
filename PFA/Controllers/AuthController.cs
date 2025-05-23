﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ✅ INSCRIPTION
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Nom) || string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.Role))
            {
                return BadRequest("Tous les champs sont obligatoires.");
            }

            if (!model.Email.Contains("@"))
                return BadRequest("Email invalide.");

            if (await _context.Admins.AnyAsync(a => a.Email == model.Email))
                return BadRequest("Cet email est déjà utilisé.");

            if (model.Password.Length < 6)
                return BadRequest("Le mot de passe doit contenir au moins 6 caractères.");

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

        // ✅ CONNEXION
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == model.Email);
            if (admin == null)
                return Unauthorized("Email ou mot de passe incorrect.");

            // Vérification du hachage
            if (!admin.MotDePasse.StartsWith("$2"))
                return BadRequest("Le mot de passe n'est pas sécurisé. Veuillez réinitialiser.");

            if (!BCrypt.Net.BCrypt.Verify(model.Password, admin.MotDePasse))
                return Unauthorized("Email ou mot de passe incorrect.");

            var token = GenerateJwtToken(admin);

            return Ok(new
            {
                token,
                role = admin.Role
            });
        }

        // ✅ JWT TOKEN
        private string GenerateJwtToken(Admin admin)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "CeciEstUneSuperCleSecreteJWT123456789");
            var issuer = _configuration["Jwt:Issuer"] ?? "https://localhost:5278";
            var audience = _configuration["Jwt:Audience"] ?? "https://localhost:5278";

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
            //    new Claim(ClaimTypes.Email, admin.Email),
            //    new Claim(ClaimTypes.Role, admin.Role),
            //    new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", admin.Role)
            //};
            var claims = new List<Claim>
            {
                new Claim("sub", admin.Id.ToString()),
                new Claim("email", admin.Email),
                new Claim("role", admin.Role)
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

    // ✅ Modèle pour l'inscription
    public class RegisterModel
    {
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    // ✅ Modèle pour la connexion
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
