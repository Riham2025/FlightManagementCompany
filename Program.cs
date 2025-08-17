using System;
using System.Collections.Generic;
using FlightManagementCompany.Models;
using FlightManagementCompany.Repository;
using FlightManagementCompany.Repository;
using FlightManagementCompany.Services;

namespace FlightManagementCompany
{
    public class Program
    {
        static void Main(string[] args)
        {
            // ===================== BOOTSTRAP =====================

            // 1)Create EF Core DbContext (adjust ctor if your context needs options)
            FlightDbContext ctx = new FlightDbContext();

            //(Optional) Ensure DB exists; if you use migrations, call ctx.Database.Migrate() instead.
            ctx.Database.EnsureCreated();

            // 2)Repositories (one per entity, as you implemented)
            Airports airportRepo = new Airports(ctx); // Repository for managing airport entities in the flight management system.
            AircraftRepository aircraftRepo = new AircraftRepository(ctx); // Repository for managing aircraft entities in the flight management system.
            RouteRepository routeRepo = new RouteRepository(ctx); // Repository for managing flight routes in the flight management system.
            FlightRepository flightRepo = new FlightRepository(ctx); // Repository for managing flight operations in the flight management system.
            PassengerRepository passengerRepo = new PassengerRepository(ctx); // Repository for managing passenger entities in the flight management system.
            BookingRepository bookingRepo = new BookingRepository(ctx); // Repository for managing booking entities in the flight management system.
            TicketRepository ticketRepo = new TicketRepository(ctx); // Repository for managing ticket entities in the flight management system.
            BaggageRepository baggageRepo = new BaggageRepository(ctx); // Repository for managing baggage entities in the flight management system.
            CrewMemberRepository crewRepo = new CrewMemberRepository(ctx); // Repository for managing crew member entities in the flight management system.
            FlightCrewRepository flightCrewRepo = new FlightCrewRepository(ctx); // Repository for managing flight crew assignments in the flight management system.
            AircraftMaintenanceRepository maintenanceRepo = new AircraftMaintenanceRepository(ctx); // Repository for managing aircraft maintenance records in the flight management system.

            //3) Services (business logic)
            IAirportService airportService = new AirportService(airportRepo); // Service for managing airport operations in the flight management system.
            IAircraftService aircraftService = new AircraftService(aircraftRepo); // Service for managing aircraft operations in the flight management system.
            IRouteService routeService = new RouteService(routeRepo, airportRepo); // Service for managing flight routes in the flight management system, including airport information.
            IFlightService flightService = new FlightService(flightRepo, routeRepo, aircraftRepo); // Service for managing flight operations in the flight management system, including route and aircraft details.
            IPassengerService passengerService = new PassengerService(passengerRepo); // Service for managing passenger operations in the flight management system.
            IBookingService bookingService = new BookingService(bookingRepo, passengerRepo, flightRepo); // Service for managing booking operations in the flight management system, including passenger and flight details.
            ITicketService ticketService = new TicketService(ticketRepo, bookingRepo, flightRepo); // Service for managing ticket operations in the flight management system, including booking and flight details.
            IBaggageService baggageService = new BaggageService(baggageRepo, passengerRepo); // Service for managing baggage operations in the flight management system, including passenger details.
            ICrewMemberService crewMemberService = new CrewMemberService(crewRepo); // Service for managing crew member operations in the flight management system.
            IFlightCrewService flightCrewService = new FlightCrewService(flightCrewRepo, flightRepo, crewRepo); // Service for managing flight crew assignments in the flight management system, including flight and crew member details.
            IAircraftMaintenanceService maintenanceService = new AircraftMaintenanceService(maintenanceRepo, aircraftRepo); // Service for managing aircraft maintenance records in the flight management system, including aircraft details.

            //  ===================== MENU LOOP =====================

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n===== Flight Management Menu =====");
                Console.WriteLine("1  - Add Airport");
                Console.WriteLine("2  - List Airports");
                Console.WriteLine("3  - Add Aircraft");
                Console.WriteLine("4  - List Aircraft");
                Console.WriteLine("5  - Create Route (Origin → Destination)");
                Console.WriteLine("6  - Create Flight");
                Console.WriteLine("7  - Register Passenger");
                Console.WriteLine("8  - Create Booking (Passenger ↔ Flight)");
                Console.WriteLine("9  - Issue Ticket (Booking + Flight)");
                Console.WriteLine("10 - Add Baggage (Passenger)");
                Console.WriteLine("11 - Add Crew Member");
                Console.WriteLine("12 - Assign Crew To Flight");
                Console.WriteLine("13 - Add Aircraft Maintenance");
                Console.WriteLine("14 - List Flights");
                Console.WriteLine("0  - Exit");
                Console.Write("Choose: ");

                string choice = Console.ReadLine() ?? string.Empty;
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            {
                                //Add Airport
                                Console.Write("IATA (3 letters): ");
                                string iata = (Console.ReadLine() ?? "").Trim();

                                Console.Write("Name: ");
                                string name = (Console.ReadLine() ?? "").Trim();

                                Console.Write("City: ");
                                string city = (Console.ReadLine() ?? "").Trim();

                                Console.Write("Country: ");
                                string country = (Console.ReadLine() ?? "").Trim();

                                Console.Write("Time zone (e.g. UTC+3): ");
                                string tz = (Console.ReadLine() ?? "").Trim();

                                string error;
                                bool ok = airportService.Create(iata, name, city, country, tz, out error);
                                Console.WriteLine(ok ? "Airport added." : $"Error: {error}");
                                break;
                            }

                        case "2":
                            {
                                //List Airports
                                List<Airport> airports = airportService.GetAll();
                                if (airports.Count == 0)
                                {
                                    Console.WriteLine("No airports.");
                                }
                                else
                                {
                                    foreach (Airport a in airports)
                                    {
                                        Console.WriteLine($"ID: {a.AirportId} | {a.IATA} - {a.Name} ({a.City}, {a.Country}) TZ:{a.TimeZone}");
                                    }
                                }
                                break;
                            }

                        case "3":
                            {
                                //Add Aircraft
                                Console.Write("Tail Number: ");
                                string tail = (Console.ReadLine() ?? "").Trim();

                                Console.Write("Model: ");
                                string model = (Console.ReadLine() ?? "").Trim();

                                int capacity = ReadInt("Capacity");

                                string error;
                                bool ok = aircraftService.Create(tail, model, capacity, out error);
                                Console.WriteLine(ok ? "Aircraft added." : $"Error: {error}");
                                break;
                            }

                        case "4":
                            {
                                //List Aircraft
                                List<Aircraft> aircraft = aircraftService.GetAll();
                                if (aircraft.Count == 0)
                                {
                                    Console.WriteLine("No aircraft.");
                                }
                                else
                                {
                                    foreach (Aircraft a in aircraft)
                                    {
                                        Console.WriteLine($"ID: {a.AircraftId} | Tail: {a.TailNumber} | Model: {a.Model} | Capacity: {a.Capacity}");
                                    }
                                }
                                break;
                            }

                        case "5":
                            {
                                //Create Route
                                int originId = ReadInt("Origin Airport ID");
                                int destId = ReadInt("Destination Airport ID");

                                string error;
                                bool ok = routeService.Create(originId, destId, out error);
                                Console.WriteLine(ok ? "Route created." : $"Error: {error}");
                                break;
                            }

                        case "6":
                            {
                                //Create Flight
                                Console.Write("Flight Number: ");
                                string flNo = (Console.ReadLine() ?? "").Trim();

                                int routeId = ReadInt("Route ID");
                                int aircraftId = ReadInt("Aircraft ID");
                                DateTime dep = ReadDate("Departure (yyyy-MM-dd HH:mm, UTC)");
                                DateTime arr = ReadDate("Arrival   (yyyy-MM-dd HH:mm, UTC)");

                                string error;
                                bool ok = flightService.Create(flNo, routeId, aircraftId, dep, arr, out error);
                                Console.WriteLine(ok ? "Flight created." : $"Error: {error}");
                                break;
                            }

                        case "7":
                            {
                                //Register Passenger
                                Console.Write("Full Name: ");
                                string name = (Console.ReadLine() ?? "").Trim();

                                Console.Write("Passport No.: ");
                                string passport = (Console.ReadLine() ?? "").Trim();

                                Console.Write("Email (optional): ");
                                string email = (Console.ReadLine() ?? "").Trim();

                                string error;
                                bool ok = passengerService.Register(name, passport, email, out error);
                                Console.WriteLine(ok ? "Passenger registered." : $"Error: {error}");
                                break;
                            }

                        case "8":
                            {
                                //Create Booking
                                int passengerId = ReadInt("Passenger ID");
                                int flightId = ReadInt("Flight ID");

                                string error;
                                bool ok = bookingService.Create(passengerId, flightId, out error);
                                Console.WriteLine(ok ? "Booking created." : $"Error: {error}");
                                break;
                            }

                        case "9":
                            {
                                //Issue Ticket
                                int bookingId = ReadInt("Booking ID");
                                int flightId = ReadInt("Flight ID");
                                decimal fare = ReadDecimal("Fare (e.g., 350.00)");
                                Console.Write("Seat (e.g., 12A): ");
                                string seat = (Console.ReadLine() ?? "").Trim();

                                string error;
                                bool ok = ticketService.Create(bookingId, flightId, fare, seat, out error);
                                Console.WriteLine(ok ? "Ticket issued." : $"Error: {error}");
                                break;
                            }

                        case "10":
                            {
                                //Add Baggage
                                int passengerId = ReadInt("Passenger ID");
                                Console.Write("Tag Number: ");
                                string tag = (Console.ReadLine() ?? "").Trim();
                                double weight = ReadDouble("Weight (kg)");

                                string error;
                                bool ok = baggageService.Add(passengerId, tag, weight, out error);
                                Console.WriteLine(ok ? "Baggage added." : $"Error: {error}");
                                break;
                            }

                        case "11":
                            {
                                //Add Crew Member
                                Console.Write("Full Name: ");
                                string name = (Console.ReadLine() ?? "").Trim();
                                Console.Write("Role (e.g., Pilot, Attendant): ");
                                string role = (Console.ReadLine() ?? "").Trim();

                                string error;
                                bool ok = crewMemberService.Create(name, role, out error);
                                Console.WriteLine(ok ? "Crew member added." : $"Error: {error}");
                                break;
                            }

                        case "12":
                            {
                                //Assign Crew → Flight
                                int flightId = ReadInt("Flight ID");
                                int crewId = ReadInt("Crew Member ID");
                                Console.Write("Assignment Role (ex: Pilot): ");
                                string role = (Console.ReadLine() ?? "").Trim();

                                string error;
                                bool ok = flightCrewService.Assign(flightId, crewId, role, out error);
                                Console.WriteLine(ok ? "Crew assigned to flight." : $"Error: {error}");
                                break;
                            }

                        case "13":
                            {
                                //Add Maintenance
                                int aircraftId = ReadInt("Aircraft ID");
                                Console.Write("Description: ");
                                string desc = (Console.ReadLine() ?? "").Trim();
                                DateTime whenUtc = ReadDate("Performed At (yyyy-MM-dd HH:mm, UTC)");

                                string error;
                                bool ok = maintenanceService.Add(aircraftId, desc, whenUtc, out error);
                                Console.WriteLine(ok ? "Maintenance record added." : $"Error: {error}");
                                break;
                            }

                        case "14":
                            {
                                //List Flights
                                List<Flight> flights = flightService.GetAll();
                                if (flights.Count == 0)
                                {
                                    Console.WriteLine("No flights.");
                                }
                                else
                                {
                                    foreach (Flight f in flights)
                                    {
                                        Console.WriteLine($"ID: {f.FlightId} | {f.FlightNumber} | Route:{f.RouteId} | Aircraft:{f.AircraftId} | {f.DepartureUtc:u} → {f.ArrivalUtc:u}");
                                    }
                                }
                                break;
                            }

                        case "0":
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    //Defensive catch so the loop doesn't crash on unexpected exceptions
                    Console.WriteLine($"Unhandled error: {ex.Message}");
                }
            }

            Console.WriteLine("Goodbye!");
        }

        // ====================== INPUT HELPERS =====================

        //Read an integer with prompt and validation
        private static int ReadInt(string label)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string raw = Console.ReadLine() ?? string.Empty;
                int value;
                if (int.TryParse(raw, out value))
                    return value;

                Console.WriteLine("Invalid number, try again.");
            }
        }

        // Read a decimal with prompt and validation
        private static decimal ReadDecimal(string label)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string raw = Console.ReadLine() ?? string.Empty;
                decimal value;
                if (decimal.TryParse(raw, out value))
                    return value;

                Console.WriteLine("Invalid amount, try again.");
            }
        }

        // Read a double with prompt and validation
        private static double ReadDouble(string label)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string raw = Console.ReadLine() ?? string.Empty;
                double value;
                if (double.TryParse(raw, out value))
                    return value;

                Console.WriteLine("Invalid number, try again.");
            }
        }

        // Read a UTC DateTime with prompt and validation
        private static DateTime ReadDate(string label)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string raw = Console.ReadLine() ?? string.Empty;
                DateTime dt;
                if (DateTime.TryParse(raw, out dt))
                    return DateTime.SpecifyKind(dt, DateTimeKind.Utc); // ensure UTC

                Console.WriteLine("Invalid date/time, try again (e.g., 2025-08-15 14:30).");
            }
        }
    }
}
