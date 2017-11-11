namespace P03_SalesDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Product
    {
        public Product()
        {
            Sale = new List<Sale>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        public ICollection<Sale> Sale { get; set; }
    }
}
