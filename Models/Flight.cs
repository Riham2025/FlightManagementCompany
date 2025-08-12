using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required] public string Status { get; set; } = "Scheduled"; // Status of the flight (e.g., "Scheduled", "Cancelled", "Delayed")


        // Navigation properties
        [ForeignKey(nameof(Route))] public int RouteId { get; set; } // Identifier for the route associated with this flight
        [ForeignKey(nameof(Aircraft))] public int AircraftId { get; set; } // Identifier for the aircraft operating this flight


    }
}
