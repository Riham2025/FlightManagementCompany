using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IRouteRepository
    {
        void Add(Route entity);
        void Delete(int id);
        List<Route> GetAll();
        Route? GetById(int id);
        void Save();
        void Update(Route entity);
    }
}