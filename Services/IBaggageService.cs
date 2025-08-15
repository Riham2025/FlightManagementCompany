using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IBaggageService // Represents a service for managing baggage operations in the flight management system.
    {
        bool Add(int passengerId, string tagNumber, double weightKg, out string error); // Adds a new baggage entry for a passenger with the specified details and validates the input.
        bool Delete(int id, out string error); // Deletes a baggage entry by its unique identifier and validates the input.
        List<Baggage> GetAll(); // Retrieves all baggage entries from the repository, including associated passengers.
        Baggage? GetById(int id); // Retrieves a baggage entry by its unique identifier, including associated passenger details.
        bool Update(Baggage baggage, out string error);
    }
}