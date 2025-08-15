using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IAircraftService
    {
        bool Create(string tailNumber, string model, int capacity, out string error);
        bool Delete(int id, out string error);
        List<Aircraft> GetAll();
        Aircraft? GetById(int id);
        bool Update(Aircraft aircraft, out string error);
    }
}