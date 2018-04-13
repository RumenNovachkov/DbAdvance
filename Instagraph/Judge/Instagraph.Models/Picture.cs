namespace Instagraph.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public decimal Size { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
