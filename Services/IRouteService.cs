using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IRouteService
    {
        bool Create(int originAirportId, int destinationAirportId, out string error);
        bool Delete(int id, out string error);
        List<Route> GetAll();
        Route? GetById(int id);
        bool Update(Route route, out string error);
    }
}