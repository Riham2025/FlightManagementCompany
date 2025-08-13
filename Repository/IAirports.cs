using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    
    public interface IAirports // Represents an interface for managing airport entities in the flight management system.
    {
        void Add(Airport entity); // Adds a new airport entity to the repository.
        void Delete(int id); // Deletes an airport entity by its unique identifier.
        List<Airport> GetAll(); // Retrieves all airport entities from the repository.
        Airport? GetByIata(string iata); // Retrieves an airport entity by its IATA code.
        Airport? GetById(int id); // Retrieves an airport entity by its unique identifier.
        void Save(); // Saves changes made to the repository.
        void Update(Airport entity);
    }
}