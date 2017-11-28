using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusTicketsSystem.Data.Models
{
    public class ArrivedTrip
    {
        public int Id { get; set; }

        public DateTime DateTimeArrived { get; set; }
        
        
        public int OriginArrivedId { get; set; }
        [NotMapped]
        public BusStation OriginArrived { get; set; }
        
        public int DestinationArrivedId { get; set; }
        [NotMapped]
        public BusStation DestinationArrived { get; set; }

        public int PassangersCounted { get; set; }
    }
}
