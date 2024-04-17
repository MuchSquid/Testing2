namespace LBW.Models.Entity
{
    public class Site
    {
        public Site() 
        {
            PlantasS = new HashSet<Planta>();
            ClienteS = new HashSet<Cliente>();
        }
        public int IdSite {  get; set; }
        public String? NameSite { get; set; }
        public String? Compania { get; set; }


        public virtual ICollection<Planta> PlantasS { get; set; }
        public virtual ICollection<Cliente> ClienteS { get; set; }
    }
}
