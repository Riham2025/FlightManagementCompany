using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Models
{
    public  class Booking
    {
        // Represents a booking made by a passenger for a flight.
        [Key] public int BookingId { get; set; } // Unique identifier for the booking
        [Required, StringLength(12)] public string BookingRef { get; set; } = null!;
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        [Required] public string Status { get; set; } = "Confirmed";
    }
}
