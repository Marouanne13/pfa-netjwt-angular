using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PFA.Models;
using PFA.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PFA.Controllers
{
    [Route("api/user/profile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserProfileService _profileService;

        public UserProfileController(UserProfileService profileService)
        {
            _profileService = profileService;
        }

        // 🔓 Méthode publique accessible sans authentification
        [HttpGet("{id}")] // Exemple: /api/user/profile/5
        public async Task<IActionResult> GetProfileById(int id)
        {
            var user = await _profileService.GetUserProfileAsync(id);
            if (user == null)
                return NotFound("Utilisateur non trouvé.");

            return Ok(new
            {
                user.Id,
                user.Nom,
                user.Email,
                user.Telephone,
                user.Adresse,
                user.DateDeNaissance,
                user.Genre,
                user.DateDeCreation
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, [FromBody] User updatedUser)
        {
            var existingUser = await _profileService.GetUserProfileAsync(id);
            if (existingUser == null)
                return NotFound("Utilisateur non trouvé.");

            // 🔄 Met à jour uniquement les champs modifiables
            existingUser.Nom = updatedUser.Nom;
            existingUser.Email = updatedUser.Email;
            existingUser.Telephone = updatedUser.Telephone;
            existingUser.Adresse = updatedUser.Adresse;
            existingUser.DateDeNaissance = updatedUser.DateDeNaissance;
            existingUser.Genre = updatedUser.Genre;

            var result = await _profileService.UpdateUserAsync(existingUser);
            if (!result)
                return StatusCode(500, "Une erreur est survenue lors de la mise à jour du profil.");

            return Ok("Profil mis à jour avec succès.");
        }

    }


}

