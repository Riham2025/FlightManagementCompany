using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;

namespace FlightManagementCompany.Services
{
    public class AircraftMaintenanceService : IAircraftMaintenanceService // Service class for managing aircraft maintenance operations in the flight management system.
    {

        private readonly AircraftMaintenanceRepository _maint; // Repository for managing aircraft maintenance records in the flight management system.
        private readonly AircraftRepository _aircraft; // Repository for managing aircraft entities in the flight management system.

        public AircraftMaintenanceService(AircraftMaintenanceRepository maint, AircraftRepository aircraft) // Constructor to initialize the service with repositories
        {
            _maint = maint; _aircraft = aircraft; // Assign the provided repositories to the private fields
        }

        public List<AircraftMaintenance> GetAll() => _maint.GetAll(); // Retrieves all aircraft maintenance records from the repository, including associated aircraft details.
        public AircraftMaintenance? GetById(int id) => _maint.GetById(id); // Retrieves a specific aircraft maintenance record by its unique identifier, including associated aircraft details.
        public List<AircraftMaintenance> GetByAircraft(int aircraftId) => _maint.GetByAircraft(aircraftId); // Retrieves all aircraft maintenance records associated with a specific aircraft by its unique identifier, including associated aircraft details.
        public List<AircraftMaintenance> GetRecent(int days) => _maint.GetRecent(days); // Retrieves recent aircraft maintenance records from the repository, filtering by the specified number of days.

        public bool Add(int aircraftId, string description, DateTime performedAtUtc, out string error) // Adds a new aircraft maintenance record for a specific aircraft and validates the input.
        {
            error = string.Empty;
            if (_aircraft.GetById(aircraftId) == null) { error = "Aircraft not found."; return false; } // Validate that the specified aircraft exists in the repository.
            if (string.IsNullOrWhiteSpace(description)) { error = "Description required."; return false; } // Validate that the description is not empty or whitespace.

            var m = new AircraftMaintenance { AircraftId = aircraftId, Description = description.Trim(), PerformedAtUtc = performedAtUtc }; // Create a new AircraftMaintenance object with the provided details.
            _maint.Add(m);  // Stage the new maintenance record for addition to the repository.
            _maint.Save(); // Save the changes to the repository after adding the new maintenance record.
            return true;
        }

        public bool Update(AircraftMaintenance maintenance, out string error) // Updates an existing aircraft maintenance record with the provided details and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (maintenance == null || maintenance.MaintenanceId <= 0) { error = "Invalid record."; return false; } // Validate the maintenance record object and its ID.
            _maint.Update(maintenance); // Stage the updated maintenance record for modification in the repository.
            _maint.Save(); // Save the updated maintenance record to the repository.
            return true;
        }

        public bool Delete(int id, out string error) // Deletes an aircraft maintenance record by its unique identifier and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            _maint.Delete(id); // Delete the maintenance record with the specified ID from the repository.
            _maint.Save(); // Save the changes to the repository after deletion.
            return true;
        }
    }
}
