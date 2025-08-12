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
        [Key] public int BaggageId { get; set; }
        [ForeignKey(nameof(Ticket))] public int TicketId { get; set; }
        [Column(TypeName = "decimal(10,2)")] public decimal WeightKg { get; set; }
        [StringLength(20)] public string? TagNumber { get; set; }
    }
}
