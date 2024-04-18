namespace LBW.Models.Entity
{
    public class Cliente
    {
        public Cliente()
        {
            PlantasC = new HashSet<Planta>();
            PlantillaC = new HashSet<Plantilla>();
            ProyectoC = new HashSet<Proyecto>();
            MuestraC = new HashSet<Muestra>();
        }
        public int IdCliente { get; set; }
        public int IdSite { get; set; }
        public String? NameCliente { get; set; }
        public String? Description { get; set; }
        public String? Address { get; set; }
        public String? Contact { get; set; }
        public DateTime? ChangedOn { get; set; }
        public String? ChangedBy { get; set; }
        public String? EmailAddrs { get; set; }
        public String? C_ClientesAgua {  get; set; }

        public virtual Site IdSiteNavigationC { get; set; }
        public virtual ICollection<Planta> PlantasC { get; set; }
        public virtual ICollection<Plantilla> PlantillaC { get; set; }
        public virtual ICollection<Proyecto> ProyectoC { get; set; }
        public virtual ICollection<Muestra> MuestraC { get; set; }

    }
}
