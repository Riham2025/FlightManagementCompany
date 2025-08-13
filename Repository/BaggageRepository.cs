using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementCompany.Repository
{
    public class BaggageRepository : IBaggageRepository// Represents a repository for managing baggage entities in the flight management system.
    {

        private readonly FlightDbContext _ctx;

        public BaggageRepository(FlightDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<Baggage> GetAll()
        {
            return _ctx.Baggage
                       .Include(b => b.Passenger)
                       .AsNoTracking()
                       .ToList();
        }

        public Baggage? GetById(int id)
        {
            return _ctx.Baggage
                       .Include(b => b.Passenger)
                       .FirstOrDefault(b => b.BaggageId == id);
        }

        public void Add(Baggage entity)
        {
            _ctx.Baggage.Add(entity);
        }

        public void Update(Baggage entity)
        {
            _ctx.Baggage.Update(entity);
        }

        public void Delete(int id)
        {
            var e = _ctx.Baggage.Find(id);
            if (e != null) _ctx.Baggage.Remove(e);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
