using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IAircraftMaintenanceService // Represents a service for managing aircraft maintenance operations in the flight management system.
    {
        bool Add(int aircraftId, string description, DateTime performedAtUtc, out string error); // Adds a new maintenance record for an aircraft with the specified details and validates the input.
        bool Delete(int id, out string error); // Deletes a maintenance record by its unique identifier and validates the input.
        List<AircraftMaintenance> GetAll();
        List<AircraftMaintenance> GetByAircraft(int aircraftId);
        AircraftMaintenance? GetById(int id);
        List<AircraftMaintenance> GetRecent(int days);
        bool Update(AircraftMaintenance maintenance, out string error);
    }
}