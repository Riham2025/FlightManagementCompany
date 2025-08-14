using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IAircraftMaintenanceRepository // Represents a repository for managing aircraft maintenance records in the flight management system.
    {
        void Add(AircraftMaintenance entity); // Stage add new aircraft maintenance record
        void Delete(int id);
        List<AircraftMaintenance> GetAll();
        List<AircraftMaintenance> GetByAircraft(int aircraftId);
        AircraftMaintenance? GetById(int id);
        List<AircraftMaintenance> GetRecent(int days);
        void Save();
        void Update(AircraftMaintenance entity);
    }
}