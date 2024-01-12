using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TruckRecords.Models;
namespace TruckRecords.Models
{
    public class TestResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestResultID { get; set; }

        public int? TruckID { get; set; }
        public int? TestID { get; set; }
        public DateTime? DateConducted { get; set; }
        public string? Result {  get; set; }
        public string? Comments { get; set; }


    }
}
