using System.Collections;
using System.Collections.Generic;

namespace BusTicketsSystem.Data.Models
{
    public class BusStation
    {
        public int BusStationId { get; set; }

        public string BusStationName { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; }

        public ICollection<Trip> DepartureTrips { get; set; } = new List<Trip>();
        public ICollection<Trip> ArrivingTrips { get; set; } = new List<Trip>();

        //public ICollection<ArrivedTrip> ArrivedTrips { get; set; } = new List<ArrivedTrip>();
    }
}
