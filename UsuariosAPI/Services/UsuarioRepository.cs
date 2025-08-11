using UsuariosAPI.Data;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuariosDbContext _context;

        public UsuarioRepository(UsuariosDbContext context)
        {
            _context = context;
        }

        public Usuario? ObtenerPorGmailYDni(string email, string dni)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Dni == dni);
        }
    }
}
