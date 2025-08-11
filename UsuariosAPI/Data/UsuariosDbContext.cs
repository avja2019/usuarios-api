using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Models;

namespace UsuariosAPI.Data
{
    public class UsuariosDbContext : DbContext
    {
        public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
