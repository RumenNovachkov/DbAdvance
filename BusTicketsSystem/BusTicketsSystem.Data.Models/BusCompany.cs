namespace BusTicketsSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BusCompany
    {
        public int BusCompanyId { get; set; }

        public string BusCompanyName { get; set; }

        public string Nationality { get; set; }

        [Range(1, 10)]
        public double Rating { get; set; }

        //public ICollection<Review> Reviews { get; set; } = new List<Review>();
        //public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}
