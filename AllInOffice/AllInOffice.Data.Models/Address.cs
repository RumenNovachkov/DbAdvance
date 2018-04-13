namespace AllInOffice.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Address
    {
        public int AddressId { get; set; }
        public string Location { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}
