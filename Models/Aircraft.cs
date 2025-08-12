using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Models
{
    public class Aircraft
    {
        // Represents an aircraft with its details.
        [Key] public int AircraftId { get; set; } // Unique identifier for the aircraft
        [Required, StringLength(20)] public string TailNumber { get; set; } = null!; // Tail number of the aircraft
        [Required] public string Model { get; set; } = null!; // Model of the aircraft
        [Range(1, 500)] public int Capacity { get; set; } // Seating capacity of the aircraft
    }
}
