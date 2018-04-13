namespace AllInOffice.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public int OrderId { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public DateTime DateRecived { get; set; }
        public bool OrderAccepted { get; set; }
        public bool OrderDelivered { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

        public int EmployeeServecingId { get; set; }
        public Employee EmployeeServicing { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
