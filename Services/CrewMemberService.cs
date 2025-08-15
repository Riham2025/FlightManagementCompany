using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;

namespace FlightManagementCompany.Services
{
    public class CrewMemberService : ICrewMemberService // Service class for managing crew member operations in the flight management system.
    {

        private readonly CrewMemberRepository _crew; // Repository for accessing crew member data
        public CrewMemberService(CrewMemberRepository crew) { _crew = crew; } // Constructor to initialize the service with a repository

        public List<CrewMember> GetAll() => _crew.GetAll(); // Retrieves all crew members from the repository, including their details such as full name and role.
        public CrewMember? GetById(int id) => _crew.GetById(id); // Retrieves a crew member by their unique identifier, including their details such as full name and role.

        public bool Create(string fullName, string role, out string error) // Creates a new crew member with the provided details and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (string.IsNullOrWhiteSpace(fullName)) { error = "Name required."; return false; } // Validate that the full name is not empty or whitespace.
            if (string.IsNullOrWhiteSpace(role)) { error = "Role required."; return false; } // Validate that the role is not empty or whitespace.
            var c = new CrewMember { FullName = fullName.Trim(), Role = role.Trim() }; // Create a new CrewMember object with the provided details.
            _crew.Add(c); _crew.Save(); // Save the new crew member to the repository.
            return true;
        }

        public bool Update(CrewMember crew, out string error) // Updates an existing crew member with the provided details and validates the input.
        {
            error = string.Empty;
            if (crew == null || crew.CrewId <= 0) { error = "Invalid crew member."; return false; }
            _crew.Update(crew); _crew.Save();
            return true;
        }

        public bool Delete(int id, out string error)
        {
            error = string.Empty;
            _crew.Delete(id); _crew.Save();
            return true;
        }
    }
}
