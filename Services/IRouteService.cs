using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IRouteService // Represents a service for managing flight routes in the flight management system.
    {
        bool Create(int originAirportId, int destinationAirportId, out string error); // Creates a new flight route with the specified origin and destination airport IDs and validates the input.
        bool Delete(int id, out string error); // Deletes a flight route by its unique identifier and validates the input.
        List<Route> GetAll(); // Retrieves all routes from the repository, including origin and destination airports.
        Route? GetById(int id); // Retrieves a route by its unique identifier, including origin and destination airports.
        bool Update(Route route, out string error);
    }
}