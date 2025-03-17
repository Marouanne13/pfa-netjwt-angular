using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PFA.Controllers
{
    [Route("api/session")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        // ✅ Stocker une valeur dans la session
        [HttpPost("set")]
        public IActionResult SetSessionValue([FromBody] SessionData data)
        {
            HttpContext.Session.SetString(data.Key, JsonSerializer.Serialize(data.Value));
            return Ok(new { Message = $"Données enregistrées : {data.Key}" });
        }

        // ✅ Récupérer une valeur de la session
        [HttpGet("get/{key}")]
        public IActionResult GetSessionValue(string key)
        {
            var value = HttpContext.Session.GetString(key);
            if (string.IsNullOrEmpty(value))
            {
                return NotFound("Donnée introuvable");
            }

            return Ok(JsonSerializer.Deserialize<object>(value));
        }

        // ✅ Supprimer une clé de session
        [HttpDelete("remove/{key}")]
        public IActionResult RemoveSessionValue(string key)
        {
            HttpContext.Session.Remove(key);
            return Ok(new { Message = $"Donnée supprimée : {key}" });
        }
    }

    public class SessionData
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
