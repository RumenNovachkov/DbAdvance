namespace BusTicketsSystem.App.Core.Commands
{
    using BusTicketsSystem.Data;
    using BusTicketsSystem.Data.Models;
    using System;
    using System.Linq;

    public class PublishReview
    {
        //Command: Publish-Review <Customer ID> <Grade> <Bus Company Name> <Content>
        public static string Execute(string[] data)
        {
            var customerId = int.Parse(data[1]);
            var grade = double.Parse(data[2]);
            var companyName = data[3];
            var content = string.Join(' ', data.Skip(4));

            using (var db = new BusTicketsDbContext())
            {
                var customer = db.Customers.SingleOrDefault(c => c.CustomerId == customerId);

                if (customer == null)
                {
                    throw new ArgumentException("Invalid Customer Number!");
                }

                var company = db.BusCompanies.SingleOrDefault(bc => bc.BusCompanyName == companyName);

                if (company == null)
                {
                    throw new ArgumentException("Invalid Company Name");
                }

                var review = new Review()
                {
                    CustomerId = customer.CustomerId,
                    BusCompanyId = company.BusCompanyId,
                    Grade = grade,
                    Content = content
                };

                db.Reviews.Add(review);

                db.SaveChanges();
                return $"Customer {customer.FirstName + " " + customer.LastName} published review for company {company.BusCompanyName}";
            }
        }
    }
}
