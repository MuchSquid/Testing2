namespace LBW.Models.Entity
{
    public class AnalisisDetalle
    {
       
        public int IdComp { get; set; }
        public int IdAnalisis { get; set; }
        public int IdUnidad { get; set; }
        public String? NameComponent { get; set; }
        public int? Version { get; set; }
        public String? AnalisisData { get; set; }
        public String? Units { get; set; }
        public int? Minimun {  get; set; }
        public int? Maximun { get; set; }
        public bool? Reportable { get; set; }
        public String? ClampLow { get; set; }
        public String? ClampHigh { get; set; }

        public virtual Analisis IdAnalisisNavigation { get; set; }
        public virtual Unidad IdUnidadNavitation { get; set; }
    }
}
