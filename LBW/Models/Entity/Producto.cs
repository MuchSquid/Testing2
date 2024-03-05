using LBW.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace LBW.Models.Entity
{
    public partial class Producto
    {
        [Key]
        public int ProductoID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}
