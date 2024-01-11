namespace TruckRecords.Models
{
    public class Component
    {

        public int ComponentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<BuildRecord> BuildRecords { get; set; }  

    }
}
