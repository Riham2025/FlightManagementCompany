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
        [ForeignKey(nameof(Booking))] public int BookingId { get; set; }
        [ForeignKey(nameof(Flight))] public int FlightId { get; set; }
    }
}
