using FlightManagementCompany.Models;

namespace FlightManagementCompany.Services
{
    public interface IFlightCrewService
    {
        bool Assign(int flightId, int crewId, string role, out string error);
        List<FlightCrew> GetAll();
        List<CrewMember> GetCrewForFlight(int flightId);
        List<Flight> GetFlightsForCrew(int crewId);
        bool Unassign(int flightId, int crewId, out string error);
    }
}