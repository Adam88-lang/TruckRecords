using TruckRecords.Models;
namespace TruckRecords.Models


{
    public class BuildRecord
    {
        public int BuildRecordID { get; set; }
        public int TruckID { get; set; }
        public int ComponentID { get; set; }
        public DateTime BuildDate { get; set; }
        public string Notes { get; set; }

        //Navigation
        public virtual Truck Truck { get; set; }
        public virtual Component Component { get; set; }

    }
}
