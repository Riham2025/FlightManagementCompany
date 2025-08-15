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

        private readonly PassengerRepository _repo;
        public PassengerService(PassengerRepository repo) { _repo = repo; }

        public List<Passenger> GetAll() => _repo.GetAll();
        public Passenger? GetById(int id) => _repo.GetById(id);

        public bool Register(string fullName, string passportNo, string email, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(fullName)) { error = "Name required."; return false; }
            if (string.IsNullOrWhiteSpace(passportNo)) { error = "Passport required."; return false; }
            if (_repo.GetAll().Any(p => p.PassportNo == passportNo.Trim())) { error = "Passport already exists."; return false; }

            var p = new Passenger { FullName = fullName.Trim(), PassportNo = passportNo.Trim(), Email = email?.Trim() };
            _repo.Add(p); _repo.Save();
            return true;
        }

        public bool Update(Passenger passenger, out string error)
        {
            error = string.Empty;
            if (passenger == null || passenger.PassengerId <= 0) { error = "Invalid passenger."; return false; }
            _repo.Update(passenger); _repo.Save();
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
