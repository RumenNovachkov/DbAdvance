namespace Stations.DataProcessor.Dto
{
    using Stations.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TrainAndSeatsDto
    {
        [Required]
        [MaxLength(10)]
        public string TrainNumber { get; set; }

        public string Type { get; set; } = "HighSpeed";

        public SeatDto[] Seats { get; set; } = new SeatDto[0];
    }
}
