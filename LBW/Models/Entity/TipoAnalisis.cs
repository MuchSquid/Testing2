using System.ComponentModel.DataAnnotations;

namespace LBW.Models.Entity
{
    public class TipoAnalisis
    {
        public TipoAnalisis() 
        { 
            Analisiss = new HashSet<Analisis>();    
        }

        public int IdTipoA { get; set; }
        public String? NombreA { get; set; }
        public String? Descripcion { get; set; }
        public bool? Removed { get; set; }

        public virtual ICollection<Analisis> Analisiss { get; set;}
    }
}
