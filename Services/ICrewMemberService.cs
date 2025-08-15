using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface ICrewMemberService // Represents a service for managing crew member operations in the flight management system.
    {
        bool Create(string fullName, string role, out string error); // Creates a new crew member with the specified full name and role, and validates the input.
        bool Delete(int id, out string error); // Deletes a crew member by their unique identifier and validates the input.
        List<CrewMember> GetAll(); // Retrieves all crew members from the repository, including their details such as full name and role.
        CrewMember? GetById(int id); // Retrieves a crew member by their unique identifier, including their details such as full name and role.
        bool Update(CrewMember crew, out string error);
    }
}