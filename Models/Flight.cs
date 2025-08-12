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
        [Required, StringLength(10)] public string FlightNumber { get; set; } = null!; // Flight number (e.g., "AA123")
        public DateTime DepartureUtc { get; set; } // Scheduled departure time in UTC
        public DateTime ArrivalUtc { get; set; } // Scheduled arrival time in UTC
        [Required] public string Status { get; set; } = "Scheduled";

    }
}
