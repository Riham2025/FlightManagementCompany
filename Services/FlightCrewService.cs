using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;

namespace FlightManagementCompany.Services
{
    public class FlightCrewService
    {

        private readonly FlightCrewRepository _repo; // Repository for managing flight crew assignments
        private readonly FlightRepository _flights; // Repository for managing flight data
        private readonly CrewMemberRepository _crew; // Repository for managing crew member data

        public FlightCrewService(FlightCrewRepository repo, FlightRepository flights, CrewMemberRepository crew) // Constructor to initialize the FlightCrewService with repositories for flight crew assignments, flights, and crew members.
        {
            _repo = repo; // Assign the provided FlightCrewRepository to the private field.
            _flights = flights; // Assign the provided FlightRepository to the private field.
            _crew = crew; // Assign the provided CrewMemberRepository to the private field.
        }

        public List<FlightCrew> GetAll() => _repo.GetAll(); // Retrieves all flight crew assignments from the repository, including associated flights and crew members.

        public bool Assign(int flightId, int crewId, string role, out string error) // Assigns a crew member to a flight with a specified role and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (_flights.GetById(flightId) == null) { error = "Flight not found."; return false; }
            if (_crew.GetById(crewId) == null) { error = "Crew member not found."; return false; }
            if (_repo.Exists(flightId, crewId)) { error = "Already assigned."; return false; }
            if (string.IsNullOrWhiteSpace(role)) { error = "Role required."; return false; }

            _repo.Add(new FlightCrew { FlightId = flightId, CrewId = crewId, Role = role.Trim() });
            _repo.Save();
            return true;
        }

        public bool Unassign(int flightId, int crewId, out string error)
        {
            error = string.Empty;
            _repo.Delete(flightId, crewId);
            _repo.Save();
            return true;
        }

        public List<CrewMember> GetCrewForFlight(int flightId) => _repo.GetCrewForFlight(flightId);
        public List<Flight> GetFlightsForCrew(int crewId) => _repo.GetFlightsForCrew(crewId);
    }
}

