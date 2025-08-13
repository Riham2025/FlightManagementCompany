using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IRouteRepository // Represents an interface for managing flight routes in the flight management system.
    {
        void Add(Route entity);
        void Delete(int id);
        List<Route> GetAll();
        Route? GetById(int id);
        void Save();
        void Update(Route entity);
    }
}