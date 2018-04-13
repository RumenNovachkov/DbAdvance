using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Stations.DataProcessor.Dto
{
    public class TripDto
    {
        [Required]
        [MaxLength(10)]
        public string Train { get; set; }

        [Required]
        [MaxLength(50)]
        public string OriginStation { get; set; }

        [Required]
        [MaxLength(50)]
        public string DestinationStation { get; set; }

        [Required]
        public string DepartureTime { get; set; }

        [Required]
        public string ArrivalTime { get; set; }

        public string Status { get; set; } = "OnTime";
        
        public string TimeDifference { get; set; }
    }
}
