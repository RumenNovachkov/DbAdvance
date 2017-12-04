namespace ProductsShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int CategoryId { get; set; }

        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; }

        public ICollection<CategoryProducts> Products { get; set; } = new List<CategoryProducts>();
    }
}
