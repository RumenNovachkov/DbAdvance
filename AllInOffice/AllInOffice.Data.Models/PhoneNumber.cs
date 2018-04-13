namespace AllInOffice.Data.Models
{
    public class PhoneNumber
    {
        public int PnoneNumberId { get; set; }

        public string PnoneNumber { get; set; }

        public int? ClientId { get; set; }
        public Client Client { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
