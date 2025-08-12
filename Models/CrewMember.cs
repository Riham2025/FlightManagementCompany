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
        [Key] public int CrewId { get; set; }
        [Required] public string FullName { get; set; } = null!;
        [Required] public string Role { get; set; } = "FlightAttendant"; // or enum
        public string? LicenseNo { get; set; }


    }
}
