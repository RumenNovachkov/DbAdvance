namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TeamBuilder.Models.Validatin;

    public class User
    {
        public int Id { get; set; }

        [MinLength(3)]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Password(6, 30, ContainsDigit = true, ContainsLowercase = true, ErrorMessage = "Invalid password")]
        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<UserTeam> Teams { get; set; } = new List<UserTeam>();
    }
}
