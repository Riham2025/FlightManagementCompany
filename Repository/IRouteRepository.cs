using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IRouteRepository // Represents an interface for managing flight routes in the flight management system.
    {
        void Add(Route entity); // Stage add new route
        void Delete(int id); // Delete a route by its unique identifier
        List<Route> GetAll(); // Retrieve all routes from the database
        Route? GetById(int id);
        void Save();
        void Update(Route entity);
    }
}