using System;
using BusTicketsSystem.Data;
using System.Collections.Generic;
using BusTicketsSystem.Data.Models;
using BusTicketsSystem.App.Core;

namespace BusTicketsSystem.App
{
    class Application
    {
        static void Main(string[] args)
        {
            //ResetDatabase();
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            Engine engine = new Engine(commandDispatcher);
            engine.Run();
        }

        private static void ResetDatabase()
        {
            using (var db = new BusTicketsDbContext())
            {
                db.Database.EnsureDeleted();
                
                db.Database.EnsureCreated();

                Seed(db);
            }
        }

        private static void Seed(BusTicketsDbContext db)
        {
            using (db)
            {
                var towns = new List<Town>()
                {
                    new Town { TownName = "Бургас", Country = "Bulgaria" },
                    new Town { TownName = "София", Country = "Bulgaria" },
                    new Town { TownName = "Варна", Country = "Bulgaria" },
                    new Town { TownName = "Плодив", Country = "Bulgaria" },
                    new Town { TownName = "Стара Загора", Country = "Bulgaria" },
                    new Town { TownName = "Русе", Country = "Bulgaria" },
                    new Town { TownName = "Каспичан", Country = "Bulgaria" },
                };

                db.Towns.AddRange(towns);

                db.SaveChanges();

                var busStations = new List<BusStation>()
                {
                    new BusStation { BusStationName = "Автогара Север Бургас", TownId = 1 },//1
                    new BusStation { BusStationName = "Автогара Юг Бургас", TownId = 1 },//2
                    new BusStation { BusStationName = "Централна автобусна спирка Бургас", TownId = 1 },//3
                    new BusStation { BusStationName = "Частна автогара София", TownId = 2 },//4
                    new BusStation { BusStationName = "Частна автогара Пловдив", TownId = 4 },//5
                    new BusStation { BusStationName = "Централна автогара София", TownId = 2 },//6
                    new BusStation { BusStationName = "Автогара Подуяне", TownId = 2 },//7
                    new BusStation { BusStationName = "Частна автогара Варна", TownId = 3 },//8
                    new BusStation { BusStationName = "Централна автогара Варна", TownId = 3 },//9
                    new BusStation { BusStationName = "Централна автогара Пловдив", TownId = 4 },//10
                    new BusStation { BusStationName = "Централна автогара Стара Загора", TownId = 5 },//11
                    new BusStation { BusStationName = "Централна автогара Русе", TownId = 6 },//12
                    new BusStation { BusStationName = "Централна автогара Каспичан", TownId = 7 },//13
                };

                db.BusStations.AddRange(busStations);

                db.SaveChanges();

                var busCompanies = new List<BusCompany>()
                {
                    new BusCompany { BusCompanyName = "TransTriumph", Nationality = "Bulgaria", Rating = 1.10 },
                    new BusCompany { BusCompanyName = "Group", Nationality = "Bulgaria", Rating = 1.10 },
                    new BusCompany { BusCompanyName = "ArdaKor", Nationality = "Bulgraria", Rating = 1.10 },
                    new BusCompany { BusCompanyName = "Autotrance 99 LTD", Nationality = "Bulgaria", Rating = 1.10 },
                    new BusCompany { BusCompanyName = "Dary Travel", Nationality = "Bulgaria", Rating = 1.10 },
                    new BusCompany { BusCompanyName = "Racic Eurobus BG", Nationality = "Bulgaria", Rating = 1.10 },
                    new BusCompany { BusCompanyName = "Agi 99", Nationality = "Bulgaria", Rating = 1.10 },
                };

                db.BusCompanies.AddRange(busCompanies);

                db.SaveChanges();

                var customers = new List<Customer>()
                {
                    new Customer { FirstName = "Иларион", LastName = "Макариополски", DateOfBirth = DateTime.Parse("2012/09/06"), Gender = Gender.Male, HomeTownId = 1 },
                    new Customer { FirstName = "Петко", LastName = "Страшника", DateOfBirth = DateTime.Parse("1948/01/06"), Gender = Gender.Male, HomeTownId = 2 },
                    new Customer { FirstName = "Jhon", LastName = "Lennon", DateOfBirth = DateTime.Parse("1980/12/08"), Gender = Gender.Male, HomeTownId = 5 },
                    new Customer { FirstName = "Георги", LastName = "Георгиев", DateOfBirth = DateTime.Parse("1988/03/18"), Gender = Gender.Male, HomeTownId = 1 },
                    new Customer { FirstName = "Иван", LastName = "Георгиев", DateOfBirth = DateTime.Parse("1988/12/06"), Gender = Gender.Male, HomeTownId = 3 },
                    new Customer { FirstName = "Митко", LastName = "Бомбата", DateOfBirth = DateTime.Parse("1990/04/22"), Gender = Gender.Male, HomeTownId = 6 },
                    new Customer { FirstName = "Коста", LastName = "Лазаров", DateOfBirth = DateTime.Parse("1984/05/03"), Gender = Gender.Male, HomeTownId = 1 },
                    new Customer { FirstName = "Росен", LastName = "Плевнелиев", DateOfBirth = DateTime.Parse("1964/05/14"), Gender = Gender.Male, HomeTownId = 2 },
                    new Customer { FirstName = "Светлин", LastName = "Насков", DateOfBirth = DateTime.Parse("1980/01/07"), Gender = Gender.Male, HomeTownId = 2 },
                    new Customer { FirstName = "Георги", LastName = "Иванов", DateOfBirth = DateTime.Parse("1940/07/02"), Gender = Gender.Male, HomeTownId = 2 },
                    new Customer { FirstName = "Богдана", LastName = "Карадочева", DateOfBirth = DateTime.Parse("1949/07/19"), Gender = Gender.Female, HomeTownId = 2 },
                    new Customer { FirstName = "Деси", LastName = "Банова", DateOfBirth = DateTime.Parse("1980/02/15"), Gender = Gender.Female, HomeTownId = 2 },
                    new Customer { FirstName = "Nithiwan", LastName = "Mason", DateOfBirth = DateTime.Parse("1985/10/29"), Gender = Gender.Female, HomeTownId = 3 },
                    new Customer { FirstName = "Сашка", LastName = "Васева", DateOfBirth = DateTime.Parse("1966/10/12"), Gender = Gender.Notspecified, HomeTownId = 7 },
                    new Customer { FirstName = "Мила", LastName = "Ризванович", DateOfBirth = DateTime.Parse("1985/04/13"), Gender = Gender.Female, HomeTownId = 1 },
                    new Customer { FirstName = "Мила", LastName = "Кунис", DateOfBirth = DateTime.Parse("1983/08/14"), Gender = Gender.Female, HomeTownId = 5 },
                    new Customer { FirstName = "Jagermeister", LastName = "Nightfury", DateOfBirth = DateTime.Parse("2016/09/23"), Gender = Gender.Female, HomeTownId = 1 },
                    new Customer { FirstName = "Вин", LastName = "Дизел", DateOfBirth = DateTime.Parse("1967/07/18"), Gender = Gender.Male, HomeTownId = 6 },
                    new Customer { FirstName = "Юри", LastName = "Гагарин", DateOfBirth = DateTime.Parse("1934/03/09"), Gender = Gender.Male, HomeTownId = 1 },
                    new Customer { FirstName = "Баба", LastName = "Вихронрав", DateOfBirth = DateTime.Parse("1944/02/16"), Gender = Gender.Female, HomeTownId = 1 },
                    new Customer { FirstName = "Николай", LastName = "Райков", DateOfBirth = DateTime.Parse("1983/06/12"), Gender = Gender.Male, HomeTownId = 2 },
                    new Customer { FirstName = "Наско", LastName = "Ментата", DateOfBirth = DateTime.Parse("1947/01/17"), Gender = Gender.Notspecified, HomeTownId = 7 },
                    new Customer { FirstName = "Ники", LastName = "Пънчев", DateOfBirth = DateTime.Parse("1972/12/18"), Gender = Gender.Male, HomeTownId = 2 },
                    new Customer { FirstName = "Дейвид", LastName = "Душевни", DateOfBirth = DateTime.Parse("1971/11/19"), Gender = Gender.Male, HomeTownId = 4 },
                    new Customer { FirstName = "Милена", LastName = "Николова", DateOfBirth = DateTime.Parse("1986/10/20"), Gender = Gender.Female, HomeTownId = 1 },
                    new Customer { FirstName = "Светла", LastName = "Петкова", DateOfBirth = DateTime.Parse("1988/09/21"), Gender = Gender.Female, HomeTownId = 3 },
                    new Customer { FirstName = "Нина", LastName = "Йолкина", DateOfBirth = DateTime.Parse("1987/08/22"), Gender = Gender.Female, HomeTownId = 6 },
                    new Customer { FirstName = "Мария", LastName = "Витанова", DateOfBirth = DateTime.Parse("1987/07/23"), Gender = Gender.Female, HomeTownId = 2 },
                    new Customer { FirstName = "Силвия", LastName = "Димитрова", DateOfBirth = DateTime.Parse("1986/06/24"), Gender = Gender.Female, HomeTownId = 5 },
                    new Customer { FirstName = "Елена", LastName = "Петрова", DateOfBirth = DateTime.Parse("1975/05/25"), Gender = Gender.Female, HomeTownId = 2 },
                    new Customer { FirstName = "Милена", LastName = "Анчева", DateOfBirth = DateTime.Parse("1978/04/26"), Gender = Gender.Female, HomeTownId = 3 },
                    new Customer { FirstName = "Калина", LastName = "Атанасова", DateOfBirth = DateTime.Parse("1997/03/27"), Gender = Gender.Female, HomeTownId = 2 },
                    new Customer { FirstName = "Роза", LastName = "Тодорова", DateOfBirth = DateTime.Parse("1987/02/28"), Gender = Gender.Female, HomeTownId = 1 },
                    new Customer { FirstName = "Carolina", LastName = "Torres", DateOfBirth = DateTime.Parse("1990/01/31"), Gender = Gender.Female, HomeTownId = 4 },
                };

                db.Customers.AddRange(customers);

                db.SaveChanges();

                var bankAccounts = new List<BankAccount>()
                {
                    new BankAccount { CustomerId = 1, Balance = 345.67m },
                    new BankAccount { CustomerId = 2, Balance = 3045.63m },
                    new BankAccount { CustomerId = 3, Balance = 3405.61m },
                    new BankAccount { CustomerId = 4, Balance = 3450.77m },
                    new BankAccount { CustomerId = 5, Balance = 960.21m },
                    new BankAccount { CustomerId = 6, Balance = 489.15m },
                    new BankAccount { CustomerId = 7, Balance = 189.65m },
                    new BankAccount { CustomerId = 8, Balance = 72.70m },
                    new BankAccount { CustomerId = 9, Balance = 83.90m },
                    new BankAccount { CustomerId = 10, Balance = 80.32m },
                    new BankAccount { CustomerId = 11, Balance = 901.46m },
                    new BankAccount { CustomerId = 12, Balance = 483.84m },
                    new BankAccount { CustomerId = 13, Balance = 76.38m },
                    new BankAccount { CustomerId = 14, Balance = 51.91m },
                    new BankAccount { CustomerId = 15, Balance = 423.53m },
                    new BankAccount { CustomerId = 16, Balance = 489.80m },
                    new BankAccount { CustomerId = 17, Balance = 6586.27m },
                    new BankAccount { CustomerId = 18, Balance = 354.72m },
                    new BankAccount { CustomerId = 19, Balance = 186.61m },
                    new BankAccount { CustomerId = 20, Balance = 48.34m },
                    new BankAccount { CustomerId = 21, Balance = 68.92m },
                    new BankAccount { CustomerId = 22, Balance = 489.46m },
                    new BankAccount { CustomerId = 23, Balance = 13.46m },
                    new BankAccount { CustomerId = 24, Balance = 105.64m },
                    new BankAccount { CustomerId = 25, Balance = 489.37m },
                    new BankAccount { CustomerId = 26, Balance = 156.73m },
                    new BankAccount { CustomerId = 27, Balance = 48.79m },
                    new BankAccount { CustomerId = 28, Balance = 1.69m },
                    new BankAccount { CustomerId = 29, Balance = 48.96m },
                    new BankAccount { CustomerId = 30, Balance = 489.07m },
                    new BankAccount { CustomerId = 31, Balance = 1001.09m },
                    new BankAccount { CustomerId = 32, Balance = 153.64m },
                    new BankAccount { CustomerId = 33, Balance = 345.08m },
                    new BankAccount { CustomerId = 34, Balance = 4658.24m }
                };

                db.BankAccounts.AddRange(bankAccounts);

                db.SaveChanges();

                var reviews = new List<Review>()
                {
                    new Review { BusCompanyId = 1, CustomerId = 1, Content = "Lorem Ipsum е елементарен примерен текст, " +
                    "използван в печатарската и типографската индустрия. " +
                    "в наши дни във софтуер за печатни издания като Aldus PageMaker, " +
                    "който включва различни версии на Lorem Ipsum.", Grade = 5.5 },
                    new Review { BusCompanyId = 2, CustomerId = 2, Content = "Lorem Ipsum е елементарен примерен текст, " +
                    "използван в печатарската и типографската индустрия. ", Grade = 4.5 },
                    new Review { BusCompanyId = 3, CustomerId = 3, Content = "Lorem Ipsum е елементарен примерен текст, " +
                    "на 20ти век със издаването на Letraset листи, съдържащи Lorem Ipsum пасажи, популярен е и " +
                    "който включва различни версии на Lorem Ipsum.", Grade = 5.3 },
                    new Review { BusCompanyId = 4, CustomerId = 4, Content = "Lorem Ipsum е елементарен примерен текст, " +
                    "използван в печатарската и типографската индустрия. ", Grade = 9.5 },
                    new Review { BusCompanyId = 5, CustomerId = 5, Content = "Lorem Ipsum е елементарен примерен текст, " +
                    "на 20ти век със издаването на Letraset листи, съдържащи Lorem Ipsum пасажи, популярен е и " +
                    "в наши дни във софтуер за печатни издания като Aldus PageMaker, " +
                    "който включва различни версии на Lorem Ipsum.", Grade = 5.1 },
                    new Review { BusCompanyId = 6, CustomerId = 6, Content = "Lorem Ipsum е елементарен примерен текст, " +
                    "в наши дни във софтуер за печатни издания като Aldus PageMaker, " +
                    "който включва различни версии на Lorem Ipsum.", Grade = 10 },
                    new Review { BusCompanyId = 7, CustomerId = 7, Content = "Lorem Ipsum е елементарен примерен текст, " +
                    "използван в печатарската и типографската индустрия. " +
                    "който включва различни версии на Lorem Ipsum.", Grade = 1 },
                };

                db.Reviews.AddRange(reviews);

                db.SaveChanges();

                var trips = new List<Trip>()
                {
                    new Trip { BusCompanyId = 1, OriginBusStationId = 1, DestinationBusStationId = 6, Status = TripStatus.Delayed, DepartureTime = DateTime.Parse("2017/11/25 11:40"), ArrivalTime = DateTime.Parse("2017/11/25 15:10") },
                    new Trip { BusCompanyId = 2, OriginBusStationId = 12, DestinationBusStationId = 7, Status = TripStatus.Cancelled, DepartureTime = DateTime.Parse("2017/11/24 08:35"), ArrivalTime = DateTime.Parse("2017/11/24 13:05") },
                    new Trip { BusCompanyId = 3, OriginBusStationId = 11, DestinationBusStationId = 10, Status = TripStatus.Cancelled, DepartureTime = DateTime.Parse("2017/11/25 18:10"), ArrivalTime = DateTime.Parse("2017/11/25 20:05") },
                    new Trip { BusCompanyId = 4, OriginBusStationId = 13, DestinationBusStationId = 4, Status = TripStatus.Arrived, DepartureTime = DateTime.Parse("2017/11/25 11:40"), ArrivalTime = DateTime.Parse("2017/11/25 15:10") },
                    new Trip { BusCompanyId = 5, OriginBusStationId = 8, DestinationBusStationId = 2, Status = TripStatus.Arrived, DepartureTime = DateTime.Parse("2017/11/21 12:00"), ArrivalTime = DateTime.Parse("2017/11/21 14:20") },

                };

                db.Trips.AddRange(trips);

                db.SaveChanges();

                var tickets = new List<Ticket>()
                {
                    new Ticket { TripId = 1, CustomerId = 1, Price = 27.60m, Seat = 1 },
                    new Ticket { TripId = 1, CustomerId = 2, Price = 27.60m, Seat = 1 },
                    new Ticket { TripId = 1, CustomerId = 3, Price = 27.60m, Seat = 1 },
                    new Ticket { TripId = 1, CustomerId = 4, Price = 27.60m, Seat = 1 },
                    new Ticket { TripId = 1, CustomerId = 5, Price = 27.60m, Seat = 1 },
                    new Ticket { TripId = 2, CustomerId = 6, Price = 34.12m, Seat = 1 },
                    new Ticket { TripId = 2, CustomerId = 7, Price = 34.12m, Seat = 1 },
                    new Ticket { TripId = 2, CustomerId = 8, Price = 34.12m, Seat = 1 },
                    new Ticket { TripId = 2, CustomerId = 9, Price = 34.12m, Seat = 1 },
                    new Ticket { TripId = 3, CustomerId = 10, Price = 16.50m, Seat = 1 },
                    new Ticket { TripId = 3, CustomerId = 11, Price = 16.50m, Seat = 1 },
                    new Ticket { TripId = 3, CustomerId = 12, Price = 16.50m, Seat = 1 },
                    new Ticket { TripId = 3, CustomerId = 13, Price = 16.50m, Seat = 1 },
                    new Ticket { TripId = 4, CustomerId = 14, Price = 12.65m, Seat = 1 },
                    new Ticket { TripId = 4, CustomerId = 15, Price = 12.65m, Seat = 1 },
                    new Ticket { TripId = 4, CustomerId = 16, Price = 12.65m, Seat = 1 },
                    new Ticket { TripId = 4, CustomerId = 17, Price = 12.65m, Seat = 1 },
                    new Ticket { TripId = 5, CustomerId = 18, Price = 14.00m, Seat = 1 },
                    new Ticket { TripId = 5, CustomerId = 19, Price = 14.00m, Seat = 1 },
                    new Ticket { TripId = 5, CustomerId = 20, Price = 14.00m, Seat = 1 },
                    new Ticket { TripId = 5, CustomerId = 21, Price = 14.00m, Seat = 1 },
                    new Ticket { TripId = 1, CustomerId = 22, Price = 27.60m, Seat = 1 },
                    new Ticket { TripId = 2, CustomerId = 23, Price = 34.12m, Seat = 1 },
                    new Ticket { TripId = 3, CustomerId = 24, Price = 16.50m, Seat = 1 },
                    new Ticket { TripId = 4, CustomerId = 25, Price = 12.65m, Seat = 1 },
                    new Ticket { TripId = 5, CustomerId = 26, Price = 14.00m, Seat = 1 },
                    new Ticket { TripId = 5, CustomerId = 27, Price = 14.00m, Seat = 1 },
                };

                db.Tickets.AddRange(tickets);

                db.SaveChanges();
            }
        }
    }
}
