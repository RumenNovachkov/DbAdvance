namespace Stations.Models
{
    using System;

    public class Trip
    {
        public int Id { get; set; }

        public int OriginStationId { get; set; }
        public Station OriginStation { get; set; }

        public int DestinationStationId { get; set; }
        public Station DestinationStation { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime
        {
            get
            {
                return this.ArrivalTime;
            }
            set
            {
                if (DateTime.Compare(value, DepartureTime) < 0)
                {
                    throw new ArgumentException("InvalidData");
                }
                this.ArrivalTime = value;
            }
        }

        public int TrainId { get; set; }
        public Train Train { get; set; }

        public TripStatus Status { get; set; }

        public TimeSpan? TimeDifference { get; set; }
    }
}
