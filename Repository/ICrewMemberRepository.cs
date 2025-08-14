using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface ICrewMemberRepository // Represents a repository for managing crew member entities in the flight management system.
    {
        void Add(CrewMember entity);// Stage add new crew member
        void Delete(int id); // Delete a crew member by its unique identifier
        List<CrewMember> GetAll(); // Retrieve all crew members
        CrewMember? GetById(int id); // Retrieve a crew member by its unique identifier
        void Save(); // Saves changes made to the database context
        void Update(CrewMember entity); // Stage update existing crew member
    }
}