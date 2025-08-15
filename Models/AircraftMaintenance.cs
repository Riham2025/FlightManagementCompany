using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Models
{
    public class AircraftMaintenance
    {
        // Represents maintenance records for an aircraft.
        [Key] public int MaintenanceId { get; set; } // Unique identifier for the maintenance record

        public string Description { get; set; } = string.Empty; // Description of the maintenance performed on the aircraft, defaulting to an empty string


        [ForeignKey(nameof(Aircraft))] public int AircraftId { get; set; } // Identifier for the aircraft associated with this maintenance record
        public DateTime MaintenanceDate { get; set; } // Date when the maintenance was performed, defaulting to the current UTC time


        [Required] public string Type { get; set; } = "A-Check"; // Type of maintenance performed (e.g., "A-Check", "B-Check", "C-Check", "D-Check")
        public string? Notes { get; set; } // Additional notes or comments about the maintenance performed


        public Aircraft Aircraft { get; set; } = null!; // Aircraft associated with this maintenance record

        public DateTime PerformedAtUtc { get; set; } // Date maintenance was performed (in UTC)

    }
}
