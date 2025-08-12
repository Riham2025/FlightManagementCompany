using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Models
{
    public class Flight
    {

        // Represents a flight with its details.
        [Key] public int FlightId { get; set; } // Unique identifier for the flight
        [Required, StringLength(10)] public string FlightNumber { get; set; } = null!;
        public DateTime DepartureUtc { get; set; }
        public DateTime ArrivalUtc { get; set; }
        [Required] public string Status { get; set; } = "Scheduled";

    }
}
