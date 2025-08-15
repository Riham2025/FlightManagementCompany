using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;


namespace FlightManagementCompany.Services
{
    public class BaggageService
    {


        private readonly BaggageRepository _baggage; // Represents a repository for managing baggage entities in the flight management system.
        private readonly PassengerRepository _passengers; // Represents a repository for managing passenger entities in the flight management system.

        public BaggageService(BaggageRepository baggage, PassengerRepository passengers) // Constructor to initialize the BaggageService with repositories for baggage and passengers.
        {
            _baggage = baggage; _passengers = passengers; // Assign the provided repositories to the private fields
        }

        public List<Baggage> GetAll() => _baggage.GetAll(); // Retrieves all baggage items from the repository, including associated passenger details.
        public Baggage? GetById(int id) => _baggage.GetById(id); // Retrieves a baggage item by its unique identifier, including associated passenger details.

        public bool Add(int passengerId, string tagNumber, double weightKg, out string error) // Creates a new baggage item for a passenger and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (_passengers.GetById(passengerId) == null) { error = "Passenger not found."; return false; } // Validate that the specified passenger exists in the repository.
            if (string.IsNullOrWhiteSpace(tagNumber)) { error = "Tag number required."; return false; } // Validate that the tag number is not empty or whitespace.
            if (weightKg <= 0) { error = "Weight must be positive."; return false; } // Validate that the weight is a positive value.

            var b = new Baggage { PassengerId = passengerId, TagNumber = tagNumber.Trim(), Weight = weightKg }; // Create a new Baggage object with the provided passenger ID, tag number, and weight.
            _baggage.Add(b); _baggage.Save(); // Stage the new baggage for addition to the repository and save it.
            return true;
        }

        public bool Update(Baggage baggage, out string error) // Updates an existing baggage item with the provided details and validates the input.
        {
            error = string.Empty; // Initialize error message to empty string.
            if (baggage == null || baggage.BaggageId <= 0) { error = "Invalid baggage."; return false; } // Validate the baggage object and its ID.
            _baggage.Update(baggage); _baggage.Save(); // Stage the updated baggage for modification in the repository and save it.
            return true;
        }

        public bool Delete(int id, out string error)
        {
            error = string.Empty;
            _baggage.Delete(id); _baggage.Save();
            return true;
        }
    }
}
