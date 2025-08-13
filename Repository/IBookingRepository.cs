using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IBookingRepository
    {
        void Add(Booking entity);
        void Delete(int id);
        List<Booking> GetAll();
        Booking? GetById(int id);
        void Save();
        void Update(Booking entity);
    }
}