namespace BusTicketsSystem.App.Core.Commands
{
    using BusTicketsSystem.Data;
    using System;
    using System.Linq;
    using BusTicketsSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class BuyTicket
    {
        //Command Buy-Ticket <Customer ID> <Trip ID> <Price> <Seat>
        public static string Execute(string[] data)
        {
            var customerId = int.Parse(data[1]);
            var tripId = int.Parse(data[2]);
            var price = decimal.Parse(data[3]);
            var seat = int.Parse(data[4]);

            using (var db = new BusTicketsDbContext())
            {
                Customer customer = db.Customers
                    .Where(c => c.CustomerId == customerId)
                    .Include(c => c.Ticket)
                    .ThenInclude(t => t.Trip)
                    .SingleOrDefault();

                if (customer == null)
                {
                    throw new ArgumentException("Invalid Customer Number!");
                }

                Trip trip = db.Trips
                    .Include(t => t.DestinationBusStation)
                    .Include(t => t.OriginBusStation)
                    .Include(t => t.BusCompany)
                    .SingleOrDefault(t => t.TripId == tripId);

                if (trip == null)
                {
                    throw new ArgumentException("Invalid Trip Number!");
                }

                if (trip.Status == TripStatus.Cancelled)
                {
                    throw new ArgumentException($"We apologize for this inconvenience but the trip " + Environment.NewLine
                        + $"from {trip.OriginBusStation.BusStationName} " + Environment.NewLine
                        + $"to {trip.DestinationBusStation.BusStationName} is cancelled");
                }
                if (db.Tickets.Any(t => t.TripId == trip.TripId && t.Seat == seat))
                {
                    throw new ArgumentException("The requested seat is already reserved, please pick another one.");
                }

                Ticket ticket = new Ticket()
                {
                    Trip = trip,
                    Customer = customer,
                    Price = price,
                    Seat = seat,
                };
                db.Tickets.Add(ticket);
                db.BankAccounts.SingleOrDefault(ba => ba.CustomerId == customer.CustomerId).Balance -= price;
                db.SaveChanges();
                return $"Customer {customer.FirstName + " " + customer.LastName} bought ticket for trip {tripId} for {price} on seat {seat}";
            }
        }
    }
}
