using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IAircraftMaintenanceService // Represents a service for managing aircraft maintenance operations in the flight management system.
    {
        bool Add(int aircraftId, string description, DateTime performedAtUtc, out string error); // Adds a new maintenance record for an aircraft with the specified details and validates the input.
        bool Delete(int id, out string error); // Deletes a maintenance record by its unique identifier and validates the input.
        List<AircraftMaintenance> GetAll(); // Retrieves all maintenance records from the repository, including associated aircraft details.
        List<AircraftMaintenance> GetByAircraft(int aircraftId); // Retrieves maintenance records for a specific aircraft by its unique identifier, including associated aircraft details.
        AircraftMaintenance? GetById(int id); // Retrieves a maintenance record by its unique identifier, including associated aircraft details.
        List<AircraftMaintenance> GetRecent(int days); // Retrieves recent maintenance records performed within the specified number of days, including associated aircraft details.
        bool Update(AircraftMaintenance maintenance, out string error); // Updates an existing maintenance record with the provided details and validates the input.
    }
}