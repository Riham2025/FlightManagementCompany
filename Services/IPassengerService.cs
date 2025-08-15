using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IPassengerService // Represents a service for managing passenger operations in the flight management system.
    {
        bool Delete(int id, out string error); // Deletes a passenger by their unique identifier and validates the input.
        List<Passenger> GetAll();
        Passenger? GetById(int id);
        bool Register(string fullName, string passportNo, string email, out string error);
        bool Update(Passenger passenger, out string error);
    }
}