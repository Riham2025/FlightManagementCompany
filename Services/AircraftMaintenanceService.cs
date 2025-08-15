using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;

namespace FlightManagementCompany.Services
{
    public class AircraftMaintenanceService
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
            if (_aircraft.GetById(aircraftId) == null) { error = "Aircraft not found."; return false; }
            if (string.IsNullOrWhiteSpace(description)) { error = "Description required."; return false; }

            var m = new AircraftMaintenance { AircraftId = aircraftId, Description = description.Trim(), PerformedAtUtc = performedAtUtc };
            _maint.Add(m); _maint.Save();
            return true;
        }

        public bool Update(AircraftMaintenance maintenance, out string error)
        {
            error = string.Empty;
            if (maintenance == null || maintenance.MaintenanceId <= 0) { error = "Invalid record."; return false; }
            _maint.Update(maintenance); _maint.Save();
            return true;
        }

        public bool Delete(int id, out string error)
        {
            error = string.Empty;
            _maint.Delete(id); _maint.Save();
            return true;
        }
    }
}
