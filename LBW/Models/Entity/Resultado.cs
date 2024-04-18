namespace LBW.Models.Entity
{
    public class Resultado
    {
        public int IdResult {  get; set; }
        public int IdSample { get; set; }
        public int IdUnidad { get; set;}
        public int IdComponent { get; set;}
        public int IdAnalysis { get; set;}
        public String? SampleNumber { get; set;}
        public decimal? ResultNumber { get; set;}
        public int? OrderNum { get; set;}
        public String? AnalysisData { get; set;}
        public String? NameComponent { get; set;}
        public String? ReportedName { get; set;}
        public String? Status { get; set;}
        public String? Reportable { get; set;}
        public DateTime? ChangedOn { get; set;}
        public String? Instrument {  get; set;}


        public virtual Muestra IdMuestraNavigationR { get; set; }
        public virtual Analisis IdAnalisisNavigationR { get; set; }
        public virtual Unidad IdUnidadNavigationR { get; set; }
    }
}
