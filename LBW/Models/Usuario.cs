using System.ComponentModel.DataAnnotations;

namespace LBW.Models
{
    public class Usuario
    {
        public String UsuarioID { get; set; }
        public String? NombreCompleto { get; set; }
        public String? Correo { get; set; }
        public bool? Rol { get; set; }
        public int? GMT_OFFSET { get; set; }
        public bool? UsuarioDeshabilitado { get; set; }
        public DateTime? FechaDeshabilitado { get; set; }
    }
}
