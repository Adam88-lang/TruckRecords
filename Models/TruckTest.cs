using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TruckRecords.Models
{
    public class TruckTest
    {
        // Use the name of the truck as the primary key
        [Key] public string TruckName { get; set; }
        public string ComponentTested { get; set; }
        public string TestType { get; set; }
        public DateTime TestDate { get; set; }
        public int Score { get; set; }

        // This will be used to populate the dropdown in the view
        public IEnumerable<SelectListItem> Truck { get; set; }
    }

}
