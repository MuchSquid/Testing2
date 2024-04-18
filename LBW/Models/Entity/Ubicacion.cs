namespace LBW.Models.Entity
{
    public class Ubicacion
    {
        public Ubicacion() 
        {
            MuestraU = new HashSet<Muestra>();
        }
        public int ID_LOCATION { get; set; }
        public String? Name_location { get; set; }
        public String? Description { get; set; }
        public String? Address { get; set; }
        public String? Contact { get; set; }

        public virtual ICollection<Muestra> MuestraU { get; set; }

    }
}
