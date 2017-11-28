namespace BusTicketsSystem.App.Core.Commands
{
    using BusTicketsSystem.Data;
    using System;
    using System.Linq;
    using System.Text;

    public class PrintReviews
    {
        //Command: Print-Reviews <Bus Company ID>
        public static string Execute(string[] data)
        {
            var companyId = int.Parse(data[1]);

            using (var db = new BusTicketsDbContext())
            {
                var company = db.BusCompanies.SingleOrDefault(c => c.BusCompanyId == companyId);

                var reviews = db.Reviews.Where(r => r.BusCompanyId == company.BusCompanyId).ToArray();

                var builder = new StringBuilder();

                foreach (var r in reviews)
                {
                    builder.AppendLine($"{r.ReviewId} {r.Grade} {r.DateAndTimeOfPublishing.Date} " +
                        $"{db.Customers.SingleOrDefault(c => c.CustomerId == r.CustomerId).FirstName + " " + db.Customers.SingleOrDefault(c => c.CustomerId == r.CustomerId).LastName} {r.Content}");
                }

                return builder.ToString().Trim();
            }
        }
    }
}
