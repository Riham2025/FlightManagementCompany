using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface ICrewMemberService // Represents a service for managing crew member operations in the flight management system.
    {
        bool Create(string fullName, string role, out string error);
        bool Delete(int id, out string error);
        List<CrewMember> GetAll();
        CrewMember? GetById(int id);
        bool Update(CrewMember crew, out string error);
    }
}