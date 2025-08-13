using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IAircraftRepository
    {
        void Add(Aircraft entity);
        void Delete(int id);
        List<Aircraft> GetAll();
        Aircraft? GetById(int id);
        void Update(Aircraft entity);
    }
}