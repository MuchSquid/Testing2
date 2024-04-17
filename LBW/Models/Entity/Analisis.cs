using System.ComponentModel.DataAnnotations;
namespace LBW.Models.Entity
{
    public class Analisis
    {
        public Analisis() 
        {
            AnalisisDetallesA = new HashSet<AnalisisDetalle>();
        }
        public int IdAnalisis { get; set; }

        [Required(ErrorMessage = "El Tipo de Analisis es obligatorio")]
        public int IdTipoA { get; set; }
        public String? NameAnalisis { get; set; }
        public int? Version {  get; set; }
        public bool? Active { get; set; }
        public String? CommonName { get; set; }
        public String? Description { get; set; }
        public String? AliasName { get; set; }
        public DateTime? ChangedOn { get; set; }
        public String? ChangedBy { get; set; }

        public virtual TipoAnalisis IdANavigation { get; set;}
        public virtual ICollection<AnalisisDetalle> AnalisisDetallesA { get; set; }
    }
}
