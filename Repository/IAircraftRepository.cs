using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IAircraftRepository // Represents an interface for managing aircraft entities in the flight management system.
    {
        void Add(Aircraft entity);
        void Delete(int id);
        List<Aircraft> GetAll();
        Aircraft? GetById(int id);
        void Update(Aircraft entity);
    }
}