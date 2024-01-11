

namespace TruckRecords.Models
{
    public class Test
    {
        public int TestID { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }

        public ICollection<TruckRecords.Models.TestResult> TestResults { get; set; }

    }
}
