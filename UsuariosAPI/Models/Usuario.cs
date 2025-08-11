namespace UsuariosAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Edad { get; set; }

        public string Dni { get; set; } = null!;
    }
}
