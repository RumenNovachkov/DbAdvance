﻿using System.Collections.Generic;

namespace HospitalApp.Data.Models
{
    public class Diagnose
    {
        public int DiagnoseId { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}
