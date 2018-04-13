namespace AllInOffice.Data.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ProductCategorie Categorie { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
