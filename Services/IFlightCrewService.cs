using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IFlightCrewService // Represents a service for managing flight crew assignments in the flight management system.
    {
        bool Assign(int flightId, int crewId, string role, out string error);  // Assigns a crew member to a flight with a specific role and validates the input.
        List<FlightCrew> GetAll(); // Retrieves all flight crew assignments from the repository, including associated flights and crew members.
        List<CrewMember> GetCrewForFlight(int flightId); // Retrieves all crew members assigned to a specific flight, including their roles.
        List<Flight> GetFlightsForCrew(int crewId); // Retrieves all flights assigned to a specific crew member, including their roles.
        bool Unassign(int flightId, int crewId, out string error); // Unassigns a crew member from a flight and validates the input.
    }
}