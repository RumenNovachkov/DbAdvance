using System;
using System.Collections.Generic;
using System.Text;

namespace AllInOffice.Data.Models
{
    public class City
    {
        public int CityId { get; set; }

        public string CityName { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
