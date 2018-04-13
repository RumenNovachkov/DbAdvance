using System;
using Stations.Data;
using Newtonsoft.Json;
using Stations.Models;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Stations.DataProcessor.Dto;
using AutoMapper;
using System.Globalization;

namespace Stations.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";

		public static string ImportStations(StationsDbContext context, string jsonString)
		{
            Station[] json = JsonConvert.DeserializeObject<Station[]>(jsonString);

            var stations = new List<Station>();

            var sb = new StringBuilder();

            foreach (var s in json)
            {
                if (string.IsNullOrWhiteSpace(s.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                if (string.IsNullOrWhiteSpace(s.Town))
                {
                    s.Town = s.Name;
                }
                if (s.Name.Length > 50 || s.Town.Length > 50)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                if (stations.Any(t => t.Name == s.Name && t.Town == s.Town))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                stations.Add(s);
                sb.AppendLine(string.Format(SuccessMessage, $"{s.Name}"));
            }

            context.Stations.AddRange(stations);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
		}

		public static string ImportClasses(StationsDbContext context, string jsonString)
		{
            var seatingClasesJson = JsonConvert.DeserializeObject<SeatingClass[]>(jsonString);

            var seatsClasses = new List<SeatingClass>();

            var sb = new StringBuilder();

            foreach (var sc in seatingClasesJson)
            {
                if (string.IsNullOrWhiteSpace(sc.Name) || string.IsNullOrWhiteSpace(sc.Abbreviation))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (sc.Name.Length > 30 || sc.Abbreviation.Length > 2)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (seatsClasses.Any(c => c.Abbreviation == sc.Abbreviation || c.Name == sc.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (context.SeatingClasses.Any(c => c.Abbreviation == sc.Abbreviation || c.Name == sc.Name))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                seatsClasses.Add(sc);
                sb.AppendLine(string.Format(SuccessMessage, $"{sc.Name}"));
            }

            context.SeatingClasses.AddRange(seatsClasses);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
		}

		public static string ImportTrains(StationsDbContext context, string jsonString)
		{
            var trainsJson = JsonConvert.DeserializeObject<TrainAndSeatsDto[]>(jsonString, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            var trains = new List<Train>();

            var sb = new StringBuilder();

            foreach (var t in trainsJson)
            {
                if (string.IsNullOrWhiteSpace(t.TrainNumber))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                if (t.TrainNumber.Length > 10)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                if (trains.Any(n => n.TrainNumber == t.TrainNumber))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                if (t.Seats.Count() == 0)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var seats = new List<TrainSeat>();

                foreach (var s in t.Seats)
                {
                    var seatClasses = context.SeatingClasses;
                    if (string.IsNullOrWhiteSpace(s.Name) || string.IsNullOrWhiteSpace(s.Abbreviation))
                    {
                        sb.AppendLine(FailureMessage);
                        break;
                    }
                    if (!seatClasses.Any(sc => sc.Name == s.Name && sc.Abbreviation == s.Abbreviation) || s.Quantity < 0)
                    {
                        sb.AppendLine(FailureMessage);
                        break;
                    }
                    int seatingClassId = context.SeatingClasses.Where(sc => sc.Name == s.Name && sc.Abbreviation == s.Abbreviation).SingleOrDefault().Id;
                    var validSeats = Mapper.Map<TrainSeat>(s);
                    validSeats.SeatingClassId = seatingClassId;
                    if (seats.Any(n => n == validSeats))
                    {
                        sb.AppendLine(FailureMessage);
                        break;
                    }
                    
                    seats.Add(validSeats);
                }
                if (seats.Count < t.Seats.Count())
                {
                    continue;
                }

                

                var type = TrainType.HighSpeed;
                if (!string.IsNullOrWhiteSpace(t.Type))
                {
                    if (t.Type == "Freight")
                    {
                        type = TrainType.Freight;
                    }
                    else if (t.Type == "LongDistance")
                    {
                        type = TrainType.LongDistance;
                    }
                    else type = TrainType.HighSpeed;
                }


                var train = new Train()
                {
                    TrainNumber = t.TrainNumber,
                    Type = type,
                    TrainSeats = seats.Select(s => new TrainSeat
                    {
                        Quantity = s.Quantity,
                        SeatingClassId = s.SeatingClassId
                    }).ToArray()
                };

                trains.Add(train);
                sb.AppendLine(string.Format(SuccessMessage, $"{train.TrainNumber}"));
            }

            context.AddRange(trains);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
		}

        public static string ImportTrips(StationsDbContext context, string jsonString)
        {
            var tripsJson = JsonConvert.DeserializeObject<TripDto[]>(jsonString, new JsonSerializer()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var sb = new StringBuilder();

            var trips = new List<Trip>();

            foreach (var t in tripsJson)
            {
                var train = context.Trains.SingleOrDefault(r => r.TrainNumber == t.Train);
                var originStation = context.Stations.SingleOrDefault(s => s.Name == t.OriginStation);
                var destinationStation = context.Stations.SingleOrDefault(s => s.Name == t.DestinationStation);

                if (train == null || originStation == null || destinationStation == null || originStation == destinationStation)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                if (string.IsNullOrWhiteSpace(t.DepartureTime.ToString()) || string.IsNullOrWhiteSpace(t.ArrivalTime.ToString()))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var depTime = DateTime.ParseExact(t.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var arTime = DateTime.ParseExact(t.ArrivalTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                if (depTime > arTime)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var status = Enum.Parse<TripStatus>(t.Status);

                var timeDiff = TimeSpan.Zero;

                if (!string.IsNullOrEmpty(t.TimeDifference))
                {
                    timeDiff = TimeSpan.ParseExact("00:00", "HH:mm", CultureInfo.InvariantCulture);
                    if (status != TripStatus.OnTime)
                    {
                        timeDiff = TimeSpan.ParseExact(t.TimeDifference, "HH:mm", CultureInfo.InvariantCulture);
                    }
                }

                var validTrip = new Trip()
                {
                    Train = train,
                    OriginStation = originStation,
                    DestinationStation = destinationStation,
                    DepartureTime = depTime,
                    ArrivalTime = arTime,
                    Status = status,
                    TimeDifference = timeDiff
                };
                trips.Add(validTrip);
                sb.AppendLine(string.Format(SuccessMessage, $"{t.OriginStation} to {t.DestinationStation}"));
            }
            context.AddRange(trips);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
            
		}

		public static string ImportCards(StationsDbContext context, string xmlString)
		{
			throw new NotImplementedException();
		}

		public static string ImportTickets(StationsDbContext context, string xmlString)
		{
			throw new NotImplementedException();
		}
	}
}