using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace UsuariosAPI.Services
{
    public class ReCaptchaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public ReCaptchaService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<bool> ValidateAsync(string token)
        {
            var secret = _config["GoogleReCaptcha:SecretKey"];
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={token}",
                null
            );

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ReCaptchaResponse>(json);

            return result != null && result.success && result.score >= 0.5;
        }

        private class ReCaptchaResponse
        {
            public bool success { get; set; }
            public float score { get; set; }
            public string action { get; set; } = null!;
        }
    }
}
