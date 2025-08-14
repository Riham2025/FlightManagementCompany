using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IFlightCrewRepository // Represents a repository for managing flight crew assignments in the flight management system.
    {
        void Add(FlightCrew entity); // Stage add new flight crew assignment
        void Delete(int flightId, int crewId); // Delete a flight crew assignment by composite key (flightId, crewId)
        bool Exists(int flightId, int crewId); // Check if a flight crew assignment exists using composite key (flightId, crewId)
        List<FlightCrew> GetAll(); // Retrieve all flight crew assignments, including linked Flight and CrewMember
        List<CrewMember> GetCrewForFlight(int flightId);
        List<Flight> GetFlightsForCrew(int crewId);
        void Save();
    }
}