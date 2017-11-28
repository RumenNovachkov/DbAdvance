using BusTicketsSystem.Data;
using BusTicketsSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusTicketsSystem.App.Core.Commands
{
    public class ChangeTripStatus
    {
        //Command: change-trip-status <Trip Id> <New Status>
        public static string Execute(string[] data)
        {
            var tripId = int.Parse(data[1]);
            var newStatus = data[2].ToLower();

            using (var db = new BusTicketsDbContext())
            {
                var newTripStatus = new TripStatus();
                //change-trip-status 2 delayed
                switch (newStatus)
                {
                    case "departed": newTripStatus = TripStatus.Departed; break;
                    case "arrived": newTripStatus = TripStatus.Arrived; break;
                    case "delayed": newTripStatus = TripStatus.Delayed; break;
                    case "cancalled": newTripStatus = TripStatus.Cancelled; break;
                    default: throw new ArgumentException("Wrong status input.");
                }

                var trip = db.Trips
                    .Where(t => t.TripId == tripId)
                    .Include(t => t.DestinationBusStation)
                    .ThenInclude(dbs => dbs.Town)
                    .Include(t => t.OriginBusStation)
                    .ThenInclude(obs => obs.Town)
                    .SingleOrDefault();

                string oldTripStatus = trip.Status.ToString();
                
                if (trip == null)
                {
                    throw new ArgumentException("Invalid Trip Number!");
                }

                if (trip.Status == newTripStatus)
                {
                    throw new ArgumentException($"The status of this trip is already {newTripStatus.ToString()}");
                }

                var builder = new StringBuilder();

                db.Trips.SingleOrDefault(t => t.TripId == trip.TripId).Status = newTripStatus;
                db.SaveChanges();

                builder.AppendLine($"Trip from {trip.OriginBusStation.Town.TownName} to {trip.DestinationBusStation.Town.TownName} on {trip.DepartureTime} Status changed from { oldTripStatus } to { newTripStatus.ToString() }");
                                
                if (newTripStatus == TripStatus.Arrived)
                {
                    var passengersCount = db.Tickets.Where(t => t.TripId == trip.TripId).Count();

                    var arrivedTrip = new ArrivedTrip()
                    {
                        DateTimeArrived = trip.ArrivalTime,
                        OriginArrivedId = trip.OriginBusStation.BusStationId,
                        OriginArrived = trip.OriginBusStation,
                        DestinationArrivedId = trip.DestinationBusStation.BusStationId,
                        DestinationArrived = trip.DestinationBusStation,
                        PassangersCounted = passengersCount
                    };
                    db.ArrivedTrips.Add(arrivedTrip);
                    db.SaveChanges();
                    builder.AppendLine($"On {arrivedTrip.DateTimeArrived.Date} - {arrivedTrip.PassangersCounted} passengers arrived at {arrivedTrip.DestinationArrived.BusStationName} " +
                        $"from {arrivedTrip.OriginArrived.BusStationName}");
                }
                return builder.ToString().Trim();
            }
        }
    }
}
