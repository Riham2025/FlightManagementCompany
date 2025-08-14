using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface ICrewMemberRepository
    {
        void Add(CrewMember entity);
        void Delete(int id);
        List<CrewMember> GetAll();
        CrewMember? GetById(int id);
        void Save();
        void Update(CrewMember entity);
    }
}