using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;

namespace FlightManagementCompany.Services
{
    public class RouteService : IRouteService // Represents a service for managing flight routes in the flight management system.
    {


        private readonly RouteRepository _routes; // Repository for accessing route data
        private readonly Airports _airports; // Repository for accessing airport data
        public RouteService(RouteRepository routes, Airports airports) { _routes = routes; _airports = airports; } // Constructor to initialize the service with repositories

        public List<Route> GetAll() => _routes.GetAll(); // Retrieves all routes from the repository, including origin and destination airports.
        public Route? GetById(int id) => _routes.GetById(id); // Retrieves a route by its unique identifier, including origin and destination airports.

        public bool Create(int originAirportId, int destinationAirportId, out string error) // Creates a new flight route with the specified origin and destination airport IDs and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (originAirportId == destinationAirportId) { error = "Origin and destination must differ."; return false; } // Validate that the origin and destination airport IDs are not the same.
            if (_airports.GetById(originAirportId) == null || _airports.GetById(destinationAirportId) == null) { error = "Airport(s) not found."; return false; } // Validate that both the origin and destination airports exist in the repository.

            var r = new Route { OriginAirportId = originAirportId, DestinationAirportId = destinationAirportId }; // Create a new Route object with the provided origin and destination airport IDs.
            _routes.Add(r); // Stage the new route for addition to the repository.
            _routes.Save(); // Save the new route to the repository.
            return true; // Return true to indicate successful creation of the route.
        }

        public bool Update(Route route, out string error) // Updates an existing flight route with the provided details and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (route == null || route.RouteId <= 0) { error = "Invalid route."; return false; } // Validate the route object and its ID.
            if (route.OriginAirportId == route.DestinationAirportId) { error = "Origin and destination must differ."; return false; } // Validate that the origin and destination airport IDs are not the same.
            _routes.Update(route); _routes.Save();
            return true;
        }

        public bool Delete(int id, out string error)
        {
            error = string.Empty;
            _routes.Delete(id); _routes.Save();
            return true;
        }
    }
}
