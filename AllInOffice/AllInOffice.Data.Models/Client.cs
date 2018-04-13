namespace AllInOffice.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Client
    {
        public int ClientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? ResponsiveEmployeeId { get; set; }
        public Employee ResponsiveEmployee { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public string CompanyName { get; set; }

        public string Bulstat { get; set; }

        public string IBan { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public bool IsDeleted { get; set; }
    }
}
