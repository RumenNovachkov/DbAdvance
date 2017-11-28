namespace BusTicketsSystem.App.Core.Commands
{
    using BusTicketsSystem.Data;
    using System;
    using System.Linq;
    using System.Text;

    public class PrintInfo
    {
        //Command: Print-Info <Bus Station ID>
        public static string Execute(string[] data)
        {
            var busStationId = int.Parse(data[1]);

            using (var db = new BusTicketsDbContext())
            {
                var busStation = db.BusStations.SingleOrDefault(bs => bs.BusStationId == busStationId);

                if (busStation == null)
                {
                    throw new ArgumentException("There is no Bus Station with that Identification Number");
                }

                var builder = new StringBuilder();

                builder.AppendLine($"{busStation.BusStationName}, {busStation.Town}");
                
                var tripsArriving = db.Trips.Where(t => t.DestinationBusStationId == busStationId).ToArray();
                if (tripsArriving.Length > 0)
                {
                    builder.AppendLine("Arrivals:");
                    foreach (var trip in tripsArriving)
                    {
                        builder.AppendLine($"From: {db.BusStations.SingleOrDefault(bs => bs.BusStationId == trip.OriginBusStationId).BusStationName} | Arrive at: {trip.ArrivalTime.Hour}:{trip.ArrivalTime.Minute} | Status: {trip.Status.ToString()}");
                    }
                }
                else
                {
                    builder.AppendLine("There is no Arriving buses today!");
                }
                var tripsDeparturing = db.Trips.Where(t => t.OriginBusStationId == busStationId).ToArray();
                if (tripsDeparturing.Length > 0)
                {
                    builder.AppendLine("Depatures:");
                    foreach (var trip in tripsDeparturing)
                    {
                        builder.AppendLine($"To: {db.BusStations.SingleOrDefault(bs => bs.BusStationId == trip.DestinationBusStationId).BusStationName} | Depart at: {trip.DepartureTime.Hour}:{trip.DepartureTime.Hour} | Status: {trip.Status.ToString()}");
                    }
                }
                else
                {
                    builder.AppendLine("There is no Departuring buses today!");
                }

                return builder.ToString().Trim();
            }
        }
    }
}
