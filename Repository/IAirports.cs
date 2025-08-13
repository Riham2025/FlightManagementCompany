using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    
    public interface IAirports 
    {
        void Add(Airport entity); // Adds a new airport entity to the repository.
        void Delete(int id);
        List<Airport> GetAll();
        Airport? GetByIata(string iata);
        Airport? GetById(int id);
        void Save();
        void Update(Airport entity);
    }
}