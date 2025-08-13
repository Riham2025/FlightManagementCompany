using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IPassengerRepository // Represents an interface for managing passenger entities in the flight management system.
    {
        void Add(Passenger entity); // Adds a new passenger entity to the repository.
        void Delete(int id);
        List<Passenger> GetAll();
        Passenger? GetById(int id);
        void Save();
        void Update(Passenger entity);
    }
}