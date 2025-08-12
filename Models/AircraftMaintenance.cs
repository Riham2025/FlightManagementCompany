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
        [Key] public int MaintenanceId { get; set; }
        [ForeignKey(nameof(Aircraft))] public int AircraftId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        [Required] public string Type { get; set; } = "A-Check";
        public string? Notes { get; set; }
        public Aircraft Aircraft { get; set; } = null!;
    }
}
