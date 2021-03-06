﻿namespace P01_HospitalDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Diagnose
    {
        public int DiagnoseId { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}
