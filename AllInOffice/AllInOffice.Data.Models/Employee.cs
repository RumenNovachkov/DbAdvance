namespace AllInOffice.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime BirthDate { get; set; }

        public MeritialStatus MeritialStatus { get; set; }

        public Gender Gender { get; set; }

        public string Position { get; set; }

        public int? PhoneNumberId { get; set; }
        public PhoneNumber PhoneNumber { get; set; }

        public string Email { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }
        
        public int? VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public DateTime HireDate { get; set; }

        public ICollection<Client> Clients { get; set; } = new List<Client>();

        public ICollection<Order> OrdersServiced { get; set; } = new List<Order>();

        public bool IsFired { get; set; }
    }
}
