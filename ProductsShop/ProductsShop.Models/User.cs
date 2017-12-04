namespace ProductsShop.Models
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public ICollection<Product> soldProducts { get; set; } = new List<Product>();
        public ICollection<Product> boughtProducts { get; set; } = new List<Product>();
    }
}
