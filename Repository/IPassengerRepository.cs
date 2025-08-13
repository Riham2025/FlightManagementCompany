using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IPassengerRepository // Represents an interface for managing passenger entities in the flight management system.
    {
        void Add(Passenger entity); // Adds a new passenger entity to the repository.
        void Delete(int id); // Deletes a passenger entity by its unique identifier.
        List<Passenger> GetAll(); //Retrieves all passenger entities from the repository.
        Passenger? GetById(int id); // Retrieves a passenger entity by its unique identifier.
        void Save();
        void Update(Passenger entity);
    }
}