using LBW.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;


namespace LBW.Models.Entity
{
    public partial class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime FechaCreacion { get; set; }

        public virtual ICollection<Cuenta> Cuentas { get; set; }
    }
}