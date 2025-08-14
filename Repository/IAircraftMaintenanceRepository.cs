using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IAircraftMaintenanceRepository // Represents a repository for managing aircraft maintenance records in the flight management system.
    {
        void Add(AircraftMaintenance entity); // Stage add new aircraft maintenance record
        void Delete(int id); // Delete an aircraft maintenance record by its unique identifier
        List<AircraftMaintenance> GetAll(); // Retrieve all aircraft maintenance records, including linked Aircraft
        List<AircraftMaintenance> GetByAircraft(int aircraftId); // Retrieve all maintenance records for a specific aircraft
        AircraftMaintenance? GetById(int id);// Retrieve a single aircraft maintenance record by its unique identifier
        List<AircraftMaintenance> GetRecent(int days); // Retrieve maintenance records performed in the last N days
        void Save(); // Saves changes made to the database context
        void Update(AircraftMaintenance entity); // Stage update existing aircraft maintenance record
    }
}