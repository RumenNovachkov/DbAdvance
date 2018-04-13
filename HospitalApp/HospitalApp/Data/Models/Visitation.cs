using System;

namespace HospitalApp.Data.Models
{
    public class Visitation
    {
        public int VisitationId { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
