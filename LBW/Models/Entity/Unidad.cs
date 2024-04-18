﻿namespace LBW.Models.Entity
{
    public class Unidad
    {
        public Unidad() 
        {
            AnalisisDetallesU = new HashSet<AnalisisDetalle>();
            ResultadosU = new HashSet<Resultado>();
        }
        public int IdUnidad { get; set; }
        public String? Nombre { get; set; }
        public String? DisplayString { get; set; }
        public String? ChangedBy { get; set; }
        public DateTime? ChangedOn { get; set; }
        public String? Removed { get; set; }
        public String? Description { get; set; }

        public virtual ICollection<AnalisisDetalle> AnalisisDetallesU { get; set; }
        public virtual ICollection<Resultado> ResultadosU { get; set; }
    }
}
