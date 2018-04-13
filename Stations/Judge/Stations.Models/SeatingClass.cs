﻿using System.ComponentModel.DataAnnotations;

namespace Stations.Models
{
    public class SeatingClass
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }
}
