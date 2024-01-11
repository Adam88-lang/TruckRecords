using TruckRecords.Models;
namespace TruckRecords.Models
{
    public class TestResult
    {
        public int TestResultID { get; set; }
        public int TruckID { get; set; }
        public int TestID { get; set; }
        public DateTime DateConducted { get; set; }
        public string Result {  get; set; }
        public string Comments { get; set; }

        //Navigation
        public  Truck Truck { get; set; }
        public  Test Test { get; set; }
    }
}
