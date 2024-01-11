namespace TruckRecords.Models
{
    public class Truck

    {
        public int TruckID { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        public DateTime AssemblyDate { get; set; }

        //Navigation
        public virtual ICollection<BuildRecord> BuildRecords { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }

    }
}
