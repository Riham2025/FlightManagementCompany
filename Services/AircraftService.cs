using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;

namespace FlightManagementCompany.Services
{
    public  class AircraftService
    {


        private readonly AircraftRepository _repo; // Repository for accessing aircraft data
        public AircraftService(AircraftRepository repo) { _repo = repo; } // Constructor to initialize the service with a repository

        public List<Aircraft> GetAll() => _repo.GetAll(); // Retrieves all aircraft from the repository.
        public Aircraft? GetById(int id) => _repo.GetById(id); // Retrieves an aircraft by its unique identifier.

        public bool Create(string tailNumber, string model, int capacity, out string error) // Creates a new aircraft with the specified details and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (string.IsNullOrWhiteSpace(tailNumber)) { error = "Tail number required."; return false; } // Validate tail number.
            if (capacity <= 0) { error = "Capacity must be positive."; return false; } // Validate capacity.
            if (_repo.GetAll().Any(a => a.TailNumber.Equals(tailNumber.Trim(), StringComparison.OrdinalIgnoreCase))) { error = "Tail number exists."; return false; } // Check if the tail number already exists in the repository.

            var a = new Aircraft { TailNumber = tailNumber.Trim().ToUpper(), Model = model?.Trim(), Capacity = capacity };
            _repo.Add(a); _repo.Save();
            return true;
        }

        public bool Update(Aircraft aircraft, out string error)
        {
            error = string.Empty;
            if (aircraft == null || aircraft.AircraftId <= 0) { error = "Invalid aircraft."; return false; }
            _repo.Update(aircraft); _repo.Save();
            return true;
        }

        public bool Delete(int id, out string error)
        {
            error = string.Empty;
            if (id <= 0) { error = "Invalid id."; return false; }
            _repo.Delete(id); _repo.Save();
            return true;
        }
    }
}
