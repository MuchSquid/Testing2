using LBW.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace LBW.Models.Entity
{
    public partial class Cuenta
    {
        [Key]
        public int CuentaID { get; set; }

        public int UsuarioID { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoCuenta { get; set; }

        [Required]
        public decimal Saldo { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
