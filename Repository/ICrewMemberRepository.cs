using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface ICrewMemberRepository // Represents a repository for managing crew member entities in the flight management system.
    {
        void Add(CrewMember entity);// Stage add new crew member
        void Delete(int id);
        List<CrewMember> GetAll();
        CrewMember? GetById(int id);
        void Save();
        void Update(CrewMember entity);
    }
}