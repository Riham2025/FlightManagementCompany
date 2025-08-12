using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Models
{
    public class Ticket
    {
        // Represents a ticket issued for a flight booking.
        [Key] public int TicketId { get; set; } // Unique identifier for the ticket
        [ForeignKey(nameof(Booking))] public int BookingId { get; set; } // Identifier for the booking associated with this ticket
        [ForeignKey(nameof(Flight))] public int FlightId { get; set; } // Identifier for the flight associated with this ticket


        // Navigation properties
        [Required, StringLength(5)] public string SeatNumber { get; set; } = null!; // Seat number assigned to the ticket (e.g., "12A")
        [Column(TypeName = "decimal(10,2)")] public decimal Fare { get; set; }
        public bool CheckedIn { get; set; }

    }
}
