namespace AllInOffice.Data.Models
{
    using System;

    public class Vehicle
    {
        public int VehicleId { get; set; }

        public string RegNumber { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public DateTime DateRegistrated { get; set; }
    }
}
