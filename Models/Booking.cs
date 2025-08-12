using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Models
{
    public  class Booking
    {
        // Represents a booking made by a passenger for a flight.
        [Key] public int BookingId { get; set; } // Unique identifier for the booking
        [Required, StringLength(12)] public string BookingRef { get; set; } = null!; // Reference number for the booking
        public DateTime BookingDate { get; set; } = DateTime.UtcNow; // Date when the booking was made, defaulting to the current UTC time
        [Required] public string Status { get; set; } = "Confirmed"; // Status of the booking (e.g., "Confirmed", "Cancelled", "Pending")


        // Navigation properties
        [ForeignKey(nameof(Passenger))] public int PassengerId { get; set; } // Identifier for the passenger who made the booking
        public Passenger Passenger { get; set; } = null!;
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
