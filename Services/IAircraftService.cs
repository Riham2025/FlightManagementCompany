using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IAircraftService // Represents a service for managing aircraft operations in the flight management system.
    {
        bool Create(string tailNumber, string model, int capacity, out string error); // Creates a new aircraft with the specified details and validates the input.
        bool Delete(int id, out string error); // Deletes an aircraft by its unique identifier and validates the input.
        List<Aircraft> GetAll(); // Retrieves all aircraft from the repository.
        Aircraft? GetById(int id); // Retrieves an aircraft by its unique identifier.
        bool Update(Aircraft aircraft, out string error); // Updates an existing aircraft with the provided details and validates the input.
    }
}