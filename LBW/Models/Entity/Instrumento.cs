namespace LBW.Models.Entity
{
    public class Instrumento
    {
        public int IdInstrumento { get; set; }
        public String? IdCodigo { get; set; }
        public String? Descripcion { get; set; }
        public String? Nombre { get; set; }
        public String? Tipo { get; set; }
        public String? Vendor { get; set; }
        public bool? Habilitado { get; set; }
        public DateTime? FechaCalibrado { get; set; }
        public DateTime? FechaCaducidad { get; set; }
    }
}
