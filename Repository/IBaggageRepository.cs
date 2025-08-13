using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IBaggageRepository
    {
        void Add(Baggage entity);
        void Delete(int id);
        List<Baggage> GetAll();
        Baggage? GetById(int id);
        void Save();
        void Update(Baggage entity);
    }
}