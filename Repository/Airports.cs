using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementCompany.Repository
{
    public class Airports
    {
        // Represents a repository for managing airports in the flight management system.
        private readonly FlightDbContext _ctx; // Database context for accessing airport data
        public Airports(FlightDbContext ctx) => _ctx = ctx;
    }
}
