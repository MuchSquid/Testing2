namespace LBW.Models.Entity
{
    public class PuntoMuestra
    {
        public int IdPm { get; set; }
        public int IdPlanta { get; set; }
        public String? NamePm { get; set; }
        public String? ChangedBy { get; set; }
        public DateTime? ChangedOn { get; set; }
        public String? Description { get; set; }
        public String? C_CodPunto {  get; set; }
        public virtual Planta IdPlantaNavigation { get; set; }
    }
}
