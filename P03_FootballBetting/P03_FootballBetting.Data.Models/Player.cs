﻿namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;

    public class Player
    {
        public int PlayerId { get; set; }

        public string Name { get; set; }

        public byte SquadNumber { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public ICollection<PlayerStatistic> PlayerStatistics { get; set; } = new List<PlayerStatistic>();

        public bool IsInjured { get; set; }
    }
}
