using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ReCaptchaService _reCaptchaService;
        private readonly UsuarioRepository _usuarioRepository;

        public AuthController(ReCaptchaService reCaptchaService, UsuarioRepository usuarioRepository)
        {
            _reCaptchaService = reCaptchaService;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // ✅ Validar reCAPTCHA usando el servicio
            var isCaptchaValid = await _reCaptchaService.ValidateAsync(request.Token);
            if (!isCaptchaValid)
                return BadRequest("❌ reCAPTCHA inválido");

            // 🔹 Aquí validas usuario y contraseña (por ahora solo un ejemplo)
            //if (request.Email == "admin@test.com" && request.Password == "1234")
            //    return Ok(new { message = "✅ Login exitoso con reCAPTCHA" });
            var usuario = _usuarioRepository.ObtenerPorGmailYDni(request.Email, request.Password);
            if (usuario == null)
                return BadRequest("❌ credencial incorrectas");

            return Ok(new
            {
                message = "✅ Login exitoso con reCAPTCHA",
                usuario = new { usuario.Id, usuario.Email }
            });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
