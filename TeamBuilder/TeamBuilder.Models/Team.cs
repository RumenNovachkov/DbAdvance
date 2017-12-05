namespace TeamBuilder.Models
{
    using System.Collections.Generic;

    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Acronym { get; set; }

        public ICollection<UserTeam> Users { get; set; } = new List<UserTeam>();
        public ICollection<TeamEvent> Events { get; set; } = new List<TeamEvent>();
    }
}
