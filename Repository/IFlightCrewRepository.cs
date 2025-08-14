using FlightManagementCompany.Models;

namespace FlightManagementCompany.Repository
{
    public interface IFlightCrewRepository
    {
        void Add(FlightCrew entity);
        void Delete(int flightId, int crewId);
        bool Exists(int flightId, int crewId);
        List<FlightCrew> GetAll();
        List<CrewMember> GetCrewForFlight(int flightId);
        List<Flight> GetFlightsForCrew(int crewId);
        void Save();
    }
}