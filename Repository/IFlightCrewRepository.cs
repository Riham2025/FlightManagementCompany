using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IFlightCrewRepository // Represents a repository for managing flight crew assignments in the flight management system.
    {
        void Add(FlightCrew entity); // Stage add new flight crew assignment
        void Delete(int flightId, int crewId);
        bool Exists(int flightId, int crewId);
        List<FlightCrew> GetAll();
        List<CrewMember> GetCrewForFlight(int flightId);
        List<Flight> GetFlightsForCrew(int crewId);
        void Save();
    }
}