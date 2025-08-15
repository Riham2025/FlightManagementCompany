using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;

namespace FlightManagementCompany.Services
{
    public class PassengerService
    {

        private readonly PassengerRepository _repo; // Repository for accessing passenger data
        public PassengerService(PassengerRepository repo) { _repo = repo; } // Constructor to initialize the service with a repository

        public List<Passenger> GetAll() => _repo.GetAll(); // Retrieves all passengers from the repository, including their details such as full name, passport number, and email.
        public Passenger? GetById(int id) => _repo.GetById(id); // Retrieves a passenger by their unique identifier, including their details such as full name, passport number, and email.

        public bool Register(string fullName, string passportNo, string email, out string error) // Registers a new passenger with the provided details and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (string.IsNullOrWhiteSpace(fullName)) // Validate that the full name is not empty or whitespace.
            { error = "Name required."; return false; // Return false to indicate that the registration failed due to an invalid name. }
            }
            if (string.IsNullOrWhiteSpace(passportNo)) { error = "Passport required."; return false; } // Validate that the passport number is not empty or whitespace.
            if (_repo.GetAll().Any(p => p.PassportNo == passportNo.Trim())) // Check if a passenger with the same passport number already exists in the repository.
            { error = "Passport already exists."; return false; // Return false to indicate that the registration failed due to a duplicate passport number. }
            }

            var p = new Passenger { FullName = fullName.Trim(), PassportNo = passportNo.Trim(), Email = email?.Trim()  // Create a new Passenger object with the provided details.
            };
            _repo.Add(p); _repo.Save(); // Save the new passenger to the repository.
            return true;
        }

        public bool Update(Passenger passenger, out string error) // Updates an existing passenger with the provided details and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (passenger == null || passenger.PassengerId <= 0) { error = "Invalid passenger."; return false; }  // Validate the passenger object and its ID.
            _repo.Update(passenger); // Save the updated passenger to the repository.
            _repo.Save();
            return true;
        }

        public bool Delete(int id, out string error)
        {
            error = string.Empty;
            _repo.Delete(id); _repo.Save();
            return true;
        }
    }
}
