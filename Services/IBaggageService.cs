using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IBaggageService // Represents a service for managing baggage operations in the flight management system.
    {
        bool Add(int passengerId, string tagNumber, double weightKg, out string error);
        bool Delete(int id, out string error);
        List<Baggage> GetAll();
        Baggage? GetById(int id);
        bool Update(Baggage baggage, out string error);
    }
}