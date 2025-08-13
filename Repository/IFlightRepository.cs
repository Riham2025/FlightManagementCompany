using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IFlightRepository 
    {
        void Add(Flight entity);
        void Delete(int id);
        List<Flight> GetAll();
        List<Flight> GetByDateRange(DateTime fromUtc, DateTime toUtc);
        Flight? GetById(int id);
        void Save();
        void Update(Flight entity);
    }
}