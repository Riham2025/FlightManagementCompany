using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Models
{
    public class Airport
    {
       
        // Represents an airport with its details.

        [Key] public int AirportId { get; set; } // Unique identifier for the airport
        [Required, StringLength(3)] public string IATA { get; set; } = null!; // IATA code (3-letter code) for the airport
        [Required] public string Name { get; set; } = null!; // Name of the airport
        [Required] public string City { get; set; } = null!; // City where the airport is located
        [Required] public string Country { get; set; } = null!; // Country where the airport is located
        public string? TimeZone { get; set; } // Time zone of the airport, optional

        // Navigation
        public ICollection<Route> OriginRoutes { get; set; } = new List<Route>(); // Routes originating from this airport
        public ICollection<Route> DestinationRoutes { get; set; } = new List<Route>(); // Routes arriving at this airport
    }
}
