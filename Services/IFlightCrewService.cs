using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IFlightCrewService // Represents a service for managing flight crew assignments in the flight management system.
    {
        bool Assign(int flightId, int crewId, string role, out string error);  // Assigns a crew member to a flight with a specific role and validates the input.
        List<FlightCrew> GetAll();
        List<CrewMember> GetCrewForFlight(int flightId);
        List<Flight> GetFlightsForCrew(int crewId);
        bool Unassign(int flightId, int crewId, out string error);
    }
}