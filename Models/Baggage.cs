using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Models
{
    public class Baggage
    {

        // Represents baggage associated with a ticket.
        [Key] public int BaggageId { get; set; } // Unique identifier for the baggage
        [ForeignKey(nameof(Ticket))] public int TicketId { get; set; } // Identifier for the ticket associated with this baggage
        [Column(TypeName = "decimal(10,2)")] public decimal WeightKg { get; set; } // Weight of the baggage in kilograms
        [StringLength(20)] public string? TagNumber { get; set; } // Baggage tag number (optional, can be null)

        public int PassengerId { get; set; } // Identifier for the passenger associated with this baggage

        // Navigation properties
        public Ticket Ticket { get; set; } = null!; // Ticket associated with this baggage
    }
}
