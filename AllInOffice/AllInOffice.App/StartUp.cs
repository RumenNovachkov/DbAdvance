using System;
using AllInOffice.Data;
using AllInOffice.Data.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Z.EntityFramework.Plus;

namespace AllInOffice.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new AllInOfficeDbContext())
            {
                //db.Database.EnsureDeleted();
                //
                //db.Database.EnsureCreated();

                //Seed(db);

                db.Employees.Update(e => new Employee { Salary = e.Salary + 10000m });
                
            }
        }

        private static void Seed(AllInOfficeDbContext db)
        {
            //var employees = new[]
            //    {
            //        new Employee { EmployeeId = 1, FirstName = "Kiro", LastName = "Pora", AddressId = 2, BirthDate = DateTime.ParseExact("1990/01/02", "yyyy/MM/dd", CultureInfo.InvariantCulture), DepartmentId = 6, Email = "kiro.p@allinoffice.com", Gender = Gender.Male, MeritialStatus = MeritialStatus.Single, Position = "Juniour Seller", Salary = 1234.56m },
            //        new Employee { EmployeeId = 2, FirstName = "Ilarion", LastName = "Makariopolski", AddressId = 3, BirthDate = DateTime.ParseExact("1987/10/20", "yyyy/MM/dd", CultureInfo.InvariantCulture), DepartmentId = 5, Email = "ilarion.m@allinoffice.com", Gender = Gender.Male, MeritialStatus = MeritialStatus.Meried, Position = "Export Packing", Salary = 1432.65m },
            //        new Employee { EmployeeId = 3, FirstName = "Minka", LastName = "Svirkata", AddressId = 4, BirthDate = DateTime.ParseExact("1993/11/02", "yyyy/MM/dd", CultureInfo.InvariantCulture), DepartmentId = 3, Email = "m.svirkata@allinoffice.com", Gender = Gender.Female, MeritialStatus = MeritialStatus.Devorced, Position = "TDM", Salary = 650.14m },
            //        new Employee { EmployeeId = 4, FirstName = "Pesho", LastName = "Pora", AddressId = 5, BirthDate = DateTime.ParseExact("1990/01/02", "yyyy/MM/dd", CultureInfo.InvariantCulture), DepartmentId = 2, Email = "pesho.pora@allinoffice.com", Gender = Gender.Male, MeritialStatus = MeritialStatus.Devorced, Position = "Chief Security", Salary = 903.26m }
            //    };
            //
            //db.Employees.AddRange(employees);
            //db.SaveChanges();
            //
            //var clients = new[]
            //{
            //        new Client { FirstName = "Svetoslav", LastName = "Nakov", Gender = Gender.Male, BirthDate = DateTime.ParseExact("1980/07/31", "yyyy/MM/dd", CultureInfo.InvariantCulture), CompanyName = "HardUni", Password = "svetlin", ResponsiveEmployeeId = 1, Email = "s.nakov@harduni.bg", Bulstat = "53464453"},
            //        new Client { FirstName = "Vladislav", LastName = "Dimanovski", Gender = Gender.Male, BirthDate = DateTime.ParseExact("1989/07/30", "yyyy/MM/dd", CultureInfo.InvariantCulture), CompanyName = "SoftSchool", Password = "vladi", ResponsiveEmployeeId = 2, Email = "v.dimanovski@softschool.ru", Bulstat = "45263584"},
            //        new Client { FirstName = "Slavin", LastName = "Drajev", Gender = Gender.Male, BirthDate = DateTime.ParseExact("1979/01/12", "yyyy/MM/dd", CultureInfo.InvariantCulture), CompanyName = "GirlsExport", Password = "slavin", ResponsiveEmployeeId = 1, Email = "slavin.drajev@girlsgonewild.org", Bulstat = "12323445"},
            //        new Client { FirstName = "Courtney", LastName = "Cocks", Gender = Gender.Female, BirthDate = DateTime.ParseExact("1964/06/15", "yyyy/MM/dd", CultureInfo.InvariantCulture), CompanyName = "Hard&Proud", Password = "cocks", ResponsiveEmployeeId = 4, Email = "cc@handp.com", Bulstat = "89787454"},
            //        new Client { FirstName = "Sofi", LastName = "Marinova", Gender = Gender.Female, BirthDate = DateTime.ParseExact("1965/04/12", "yyyy/MM/dd", CultureInfo.InvariantCulture), CompanyName = "CS", Password = "lorenco", ResponsiveEmployeeId = 2, Email = "s.marinova@ciganskosarce.bg", Bulstat = "25853674"}
            //    };
            //
            //db.Clients.AddRange(clients);
            //db.SaveChanges();

            //var vehicles = new[]
            //{
            //        new Vehicle { Brand = "VW", Model = "Caddy", RegNumber = "C1234AC", DateRegistrated = DateTime.ParseExact("2016/01/01", "yyyy/MM/dd", CultureInfo.InvariantCulture)},
            //        new Vehicle { Brand = "Ferrari", Model = "Enzo", RegNumber = "C4321AC", DateRegistrated = DateTime.ParseExact("2014/04/04", "yyyy/MM/dd", CultureInfo.InvariantCulture)},
            //        new Vehicle { Brand = "Lada", Model = "2105", RegNumber = "A1623KA", DateRegistrated = DateTime.ParseExact("2015/03/03", "yyyy/MM/dd", CultureInfo.InvariantCulture)},
            //        new Vehicle { Brand = "VW", Model = "Paddy", RegNumber = "CB1234AC", DateRegistrated = DateTime.ParseExact("2017/02/02", "yyyy/MM/dd", CultureInfo.InvariantCulture)},
            //    };
            //
            //db.Vehicles.AddRange(vehicles);
            //db.SaveChanges();

            //var phoneNumbers = new[]
            //{
            //        new PhoneNumber {PnoneNumber = "056858888", EmployeeId = 1},
            //        new PhoneNumber {PnoneNumber = "0877784512", EmployeeId = 2},
            //        new PhoneNumber {PnoneNumber = "0877235689", EmployeeId = 3},
            //        new PhoneNumber {PnoneNumber = "0898546210", ClientId = 1},
            //        new PhoneNumber {PnoneNumber = "0898546215", ClientId = 1},
            //        new PhoneNumber {PnoneNumber = "0886887763", ClientId = 2},
            //        new PhoneNumber {PnoneNumber = "0886887762", ClientId = 2},
            //        new PhoneNumber {PnoneNumber = "0888654832", ClientId = 3},
            //        new PhoneNumber {PnoneNumber = "0886887761", ClientId = 2}
            //    };
            //
            //db.PhoneNumbers.AddRange(phoneNumbers);
            //db.SaveChanges();
            //
            //var orders = new[]
            //{
            //        new Order { ClientId = 1, DateRecived = DateTime.ParseExact("2017/11/21", "yyyy/MM/dd", CultureInfo.InvariantCulture), AddressId = 6, EmployeeServecingId = 1},
            //        new Order { ClientId = 2, DateRecived = DateTime.ParseExact("2017/11/12", "yyyy/MM/dd", CultureInfo.InvariantCulture), AddressId = 7, EmployeeServecingId = 2, OrderAccepted = true, OrderDelivered = true},
            //    };
            //
            //db.Orders.AddRange(orders);
            //db.SaveChanges();
        }
    }
}
