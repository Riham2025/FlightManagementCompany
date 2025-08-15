using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface ICrewMemberService
    {
        bool Create(string fullName, string role, out string error);
        bool Delete(int id, out string error);
        List<CrewMember> GetAll();
        CrewMember? GetById(int id);
        bool Update(CrewMember crew, out string error);
    }
}