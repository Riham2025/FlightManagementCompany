using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Models
{
    public class Passenger
    {
        // Represents a passenger with their details.
        [Key] public int PassengerId { get; set; }
        [Required] public string FullName { get; set; } = null!;
        [Required, StringLength(20)] public string PassportNo { get; set; } = null!;
        public string Nationality { get; set; } = "US";
        public DateTime DOB { get; set; }
    }
}
