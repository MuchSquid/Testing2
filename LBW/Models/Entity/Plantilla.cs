namespace LBW.Models.Entity
{
    public class Plantilla
    {
        public Plantilla()
        {
            PlantillaDetalleP = new HashSet<PlantillaDetalle>();
            ProyectoP = new HashSet<Proyecto>();
        }
        public int IdTL { get; set; }
        public int IdCliente { get; set; }
        public String? NameTlist { get; set; }
        public String? Description { get; set; }
        public DateTime? ChangedOn { get; set; }
        public String? ChangedBy { get; set; }
        public bool? Removed {  get; set; }
        

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual ICollection<PlantillaDetalle> PlantillaDetalleP { get; set; }
        public virtual ICollection<Proyecto> ProyectoP { get; set; }
    }
}
