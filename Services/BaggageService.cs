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

        public BaggageService(BaggageRepository baggage, PassengerRepository passengers)
        {
            _baggage = baggage; _passengers = passengers;
        }

        public List<Baggage> GetAll() => _baggage.GetAll();
        public Baggage? GetById(int id) => _baggage.GetById(id);

        public bool Add(int passengerId, string tagNumber, double weightKg, out string error)
        {
            error = string.Empty;
            if (_passengers.GetById(passengerId) == null) { error = "Passenger not found."; return false; }
            if (string.IsNullOrWhiteSpace(tagNumber)) { error = "Tag number required."; return false; }
            if (weightKg <= 0) { error = "Weight must be positive."; return false; }

            var b = new Baggage { PassengerId = passengerId, TagNumber = tagNumber.Trim(), Weight = weightKg };
            _baggage.Add(b); _baggage.Save();
            return true;
        }

        public bool Update(Baggage baggage, out string error)
        {
            error = string.Empty;
            if (baggage == null || baggage.BaggageId <= 0) { error = "Invalid baggage."; return false; }
            _baggage.Update(baggage); _baggage.Save();
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
