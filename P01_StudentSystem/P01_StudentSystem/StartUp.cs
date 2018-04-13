using System;
using P01_StudentSystem.Data;
using P01_StudentSystem.Data.Models;
using System.Linq;

namespace P01_StudentSystem
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new StudentSystemContext())
            {
                PrepareDb(db);

                var courses = db.Courses
                    .Select(e => new
                    {
                        e.Name,
                        e.Description,
                        e.StartDate,
                        e.EndDate,
                        e.Price
                    })
                    .OrderBy(c => c.Name)
                    .ToArray();

                foreach (var c in courses)
                {
                    Console.WriteLine($"--Course name: {c.Name}");
                    Console.WriteLine($"----Starting at: {c.StartDate.ToShortDateString()} and Finish at: {c.EndDate.ToShortDateString()}");
                    Console.WriteLine("-" + c.Description);
                    Console.WriteLine($"Price: {c.Price:f2}");
                    Console.WriteLine();
                }
            }
        }

        private static void PrepareDb(StudentSystemContext db)
        {
            db.Database.EnsureDeleted();

            db.Database.EnsureCreated();

            Seed(db);
        }

        private static void Seed(StudentSystemContext db)
        {
            var students = new[]
            {
                new Student("Pesho Nakov", "0888888888", DateTime.Parse("2014/03/03"), DateTime.Parse("1990/03/03")),
                new Student("Gosho Peshov", "0899999999", DateTime.Parse("2015/06/03"), DateTime.Parse("1988/03/10")),
                new Student("Mimi Kapsuzova", "0877777777", DateTime.Parse("2016/06/27"), DateTime.Parse("1997/12/27")),
                new Student("Ani Lebedkova", "0878787878", DateTime.Parse("2013/09/11"), DateTime.Parse("1985/08/01")),
                new Student("Kolyo Mvrudiev", "0898989898", DateTime.Parse("2015/10/07"), DateTime.Parse("1990/02/06"))
            };

            db.Students.AddRange(students);

            var courses = new[]
            {
                new Course("Kachestveni narkotici", "Ey sq sh'e gi nauchite!", "2017/01/10", "2017/06/10", 3475.45m),
                new Course("Prostituciq", "Nachalen kurs za kurvi i svodnici. (Kontraceptivite sa za vasha smetka)", "2017/02/15", "2017/02/26", 475.99m),
                new Course("Smurtonosno orujie", "Nay-dobriq kurs za boravene s orujie na ulicata! Budi s nas, ostani jiv", "2017/01/10", "2018/01/10", 8975.90m),
                new Course("Drebna prestupnost ili kak da pravim pari ot prosqci", "Razkriyte taynite na drebnata organizirana prestupnost", "2017/09/12", "2017/12/10", 5175.19m),
            };

            db.Courses.AddRange(courses);

            db.SaveChanges();
        }
    }
}
