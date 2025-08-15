using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IAirportService // Represents a service for managing airport operations in the flight management system.
    {
        bool Create(string iata, string name, string city, string country, string timeZone, out string error); // Creates a new airport with the specified details and validates the input.
        bool Delete(int id, out string error); // Deletes an airport by its unique identifier and validates the input.
        List<Airport> GetAll(); // Retrieves all airports from the repository.
        Airport? GetById(int id);
        bool Update(Airport airport, out string error);
    }
}