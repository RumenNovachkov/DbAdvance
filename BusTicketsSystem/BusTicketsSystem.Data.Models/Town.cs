namespace BusTicketsSystem.Data.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public int TownId { get; set; }

        public string TownName { get; set; }

        public string Country { get; set; }

        //public ICollection<BusStation> BusStations { get; set; } = new List<BusStation>();
        //public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
