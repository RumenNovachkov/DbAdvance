using DataBaseFirstDemoSqlServer.Data;
using DataBaseFirstDemoSqlServer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataBaseFirstDemoSqlServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SoftUniDbContext())
            {
                //var town = db.Towns.FirstOrDefault(t => t.Name == "Gabrovo");
                //town.Name = "Pleven dage";
                //db.SaveChanges();

                //var towns = db
                //        .Towns
                //        .Include(t => t.Addresses)
                //        .ThenInclude(e => e.Employees)
                //        .OrderByDescending(t => t.Addresses.Count)
                //        .ToList();
                //
                //foreach (var town in towns)
                //{
                //    Console.WriteLine($"{town.Name} {town.Addresses.Count}");
                //    foreach (var add in town.Addresses)
                //    {
                //        Console.WriteLine("   " + add.AddressText);
                //        foreach (var emp in add.Employees)
                //        {
                //            Console.WriteLine($"      {emp.FirstName} {emp.LastName}");
                //        }
                //    }
                //}


                //var town = new Town
                //{
                //    Name = "Gabrovo"
                //};
                //
                //var address = new Address
                //{
                //    AddressText = "ul. Shazam 1"
                //};
                //
                //town.Addresses.Add(address);
                //db.Towns.Add(town);
                //db.SaveChanges();


                //var employees = db.Employees.Select(
                //e => new
                //{
                //    e.FirstName,
                //    e.LastName,
                //    e.MiddleName,
                //    e.JobTitle,
                //    e.Salary,
                //    e.DepartmentId
                //})
                //.Include()
                //.Where(e => e.DepartmentId == 6)
                //.OrderBy(e => e.Salary)
                //.ThenByDescending(e => e.FirstName)
                //.ToList();

                var employees = db.Departments
                                  .Include(e => e.Employees).Where(e => e.Name == "Research and Development")
                                  .ToList();


                foreach (var e in employees)
                {
                    foreach (var emp in e.Employees.OrderBy(emp => emp.Salary).ThenByDescending(emp => emp.FirstName))
                    {
                        Console.WriteLine($"{emp.FirstName} {emp.LastName} from {e.Name} - ${emp.Salary:f2}");
                    }
                    
                }
            } 
        }
    }
}
