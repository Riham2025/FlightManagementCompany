using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;

namespace FlightManagementCompany.Services
{
    public class AirportService : IAirportService // Represents a service for managing airport operations in the flight management system.
    {

        private readonly Airports _repo; // Repository for accessing airport data
        public AirportService(Airports repo) { _repo = repo; } // Constructor to initialize the service with a repository

        public List<Airport> GetAll() => _repo.GetAll(); // Retrieves all airports from the repository.
        public Airport? GetById(int id) => _repo.GetById(id); // Retrieves an airport by its unique identifier.

        public bool Create(string iata, string name, string city, string country, string timeZone, out string error) // Creates a new airport with the specified details and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (string.IsNullOrWhiteSpace(iata) || iata.Trim().Length != 3) { error = "IATA must be 3 letters."; return false; } // Validate IATA code length.
            if (string.IsNullOrWhiteSpace(name)) { error = "Airport name required."; return false; } // Validate airport name.
            //prevent duplicates
            if (_repo.GetAll().Any(a => a.IATA.Equals(iata.Trim(), StringComparison.OrdinalIgnoreCase))) { error = "IATA already exists."; return false; } // Check if the IATA code already exists in the repository.

            var a = new Airport { IATA = iata.Trim().ToUpper(), Name = name.Trim(), City = city?.Trim(), Country = country?.Trim(), TimeZone = timeZone?.Trim() }; // Create a new Airport object with the provided details.
            _repo.Add(a); _repo.Save(); // Save the new airport to the repository.
            return true; // Return true to indicate successful creation of the airport.
        }

        public bool Update(Airport airport, out string error) // Updates an existing airport with the provided details and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (airport == null || airport.AirportId <= 0) { error = "Invalid airport."; return false; } // Validate the airport object and its ID.
            _repo.Update(airport); _repo.Save(); // Save the updated airport to the repository.
            return true; // Return true to indicate successful update of the airport.
        }

        public bool Delete(int id, out string error) // Deletes an airport by its unique identifier and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (id <= 0) { error = "Invalid id."; return false; } // Validate the ID of the airport to be deleted.
            _repo.Delete(id); _repo.Save(); // Save the changes to the repository after deletion.
            return true; // Return true to indicate successful deletion of the airport.
        }
    }
}
