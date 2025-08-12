using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Models
{
    public class CrewMember
    {
        // Represents a crew member working on a flight.
        [Key] public int CrewId { get; set; } // Unique identifier for the crew member
        [Required] public string FullName { get; set; } = null!; // Full name of the crew member
        [Required] public string Role { get; set; } = "FlightAttendant"; // or enum // Role of the crew member (e.g., "Pilot", "Flight Attendant", "Engineer")
        public string? LicenseNo { get; set; }


    }
}
