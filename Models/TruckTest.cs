using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TruckRecords.Models
{
    public class TruckTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TruckTestID { get; set; }    

        [Required(ErrorMessage = "Truck name is required")]
        public string TruckName { get; set; }

        [Required(ErrorMessage = "Component tested is required")]
        public string ComponentTested { get; set; }

        [Required(ErrorMessage = "Test type is required")]
        public string TestType { get; set; }

        [Required(ErrorMessage = "Test date is required")]
        [DataType(DataType.Date)]
        public DateTime TestDate { get; set; }

        [Required(ErrorMessage = "Score is required")]
        [Range(1, 10, ErrorMessage = "Score must be between 1 and 10")]
        public int Score { get; set; }

        // This property doesn't need validation as it's for view purposes
        public IEnumerable<SelectListItem>? Trucks { get; set; }
    }
}
