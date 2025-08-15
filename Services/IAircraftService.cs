using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IAircraftService // Represents a service for managing aircraft operations in the flight management system.
    {
        bool Create(string tailNumber, string model, int capacity, out string error); // Creates a new aircraft with the specified details and validates the input.
        bool Delete(int id, out string error);
        List<Aircraft> GetAll();
        Aircraft? GetById(int id);
        bool Update(Aircraft aircraft, out string error);
    }
}