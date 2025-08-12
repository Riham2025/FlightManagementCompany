using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Models
{
    // Join table (many-to-many): Flight <-> CrewMember
    public class FlightCrew
    {
        // Represents the assignment of a crew member to a flight.
        public int FlightId { get; set; } // Unique identifier for the flight
        public int CrewId { get; set; }// Unique identifier for the crew member
        [Required] public string RoleOnFlight { get; set; } = "Attendant";
    }
}
