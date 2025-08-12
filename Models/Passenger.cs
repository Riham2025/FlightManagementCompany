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
        [Key] public int PassengerId { get; set; } // Unique identifier for the passenger
        [Required] public string FullName { get; set; } = null!; // Full name of the passenger
        [Required, StringLength(20)] public string PassportNo { get; set; } = null!; // Passport number of the passenger

        
        public string Nationality { get; set; } = "US";  
        public DateTime DOB { get; set; } // Date of birth of the passenger


        // Navigation properties
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>(); // Bookings made by this passenger
    }
}
