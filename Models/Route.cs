using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Models
{
    public class Route
    {
        // Represents a flight route between two airports.
        [Key] public int RouteId { get; set; } // Unique identifier for the route
        [ForeignKey(nameof(Origin))] public int OriginAirportId { get; set; } // Identifier for the origin airport
        [ForeignKey(nameof(Destination))] public int DestinationAirportId { get; set; } // Identifier for the destination airport
        [Range(1, 20000)] public int DistanceKm { get; set; }
    }
}
