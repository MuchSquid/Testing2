using System.ComponentModel.DataAnnotations;

namespace LBW.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
