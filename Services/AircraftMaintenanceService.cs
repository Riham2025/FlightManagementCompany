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
        private readonly AircraftRepository _aircraft;

        public AircraftMaintenanceService(AircraftMaintenanceRepository maint, AircraftRepository aircraft)
        {
            _maint = maint; _aircraft = aircraft;
        }

        public List<AircraftMaintenance> GetAll() => _maint.GetAll();
        public AircraftMaintenance? GetById(int id) => _maint.GetById(id);
        public List<AircraftMaintenance> GetByAircraft(int aircraftId) => _maint.GetByAircraft(aircraftId);
        public List<AircraftMaintenance> GetRecent(int days) => _maint.GetRecent(days);

        public bool Add(int aircraftId, string description, DateTime performedAtUtc, out string error)
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
