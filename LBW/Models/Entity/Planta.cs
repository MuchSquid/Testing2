namespace LBW.Models.Entity
{
    public class Planta
    {
        public Planta() 
        {
            PuntoMuestrasP = new HashSet<PuntoMuestra>();
        }

        public int IdPlanta { get; set; }
        public int IdCliente { get; set; }
        public int IdSite { get; set; }
        public String? NamePl { get; set; }
        public String? ChangedBy { get; set; }
        public DateTime? ChangedOn { get; set; }
        public bool? Removed { get; set; }
        public String? Description { get; set; }

        public virtual Site IdSiteNavigationP { get; set; }
        public virtual Cliente IdClienteNavigationP { get; set; }
        public virtual ICollection<PuntoMuestra> PuntoMuestrasP { get; set; }
    }

}
