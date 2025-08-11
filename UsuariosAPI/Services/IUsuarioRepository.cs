using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public interface IUsuarioRepository
    {
        Usuario? ObtenerPorGmailYDni(string gmail, string dni);
    }
}
