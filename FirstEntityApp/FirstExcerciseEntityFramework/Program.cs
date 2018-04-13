using System;
using System.Linq;
using FirstExcerciseEntityFramework.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FirstExcerciseEntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            //var db = new SoftUniDbContext();
            //3-------------------------------------------------------------------------------------------------------------------------
            //Employee employee = null;
            //employee = db.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            //
            //var newAddress = new Address()
            //{
            //    AddressText = "Vitoshka 15",
            //    TownId = 4
            //};
            //
            //db.Addresses.Add(newAddress);
            //db.SaveChanges();
            //employee.Address = db.Addresses.FirstOrDefault(a => a.AddressText == "Vitoshka 15");
            //db.SaveChanges();

            //var employees = db.Employees
            //    .Include(a => a.Address)
            //                  .Select(e => new
            //                  {
            //                      e.Address.AddressText,
            //                      e.Address.AddressId
            //                  })
            //                  .OrderByDescending(e => e.AddressId)
            //                  .ToList();
            //
            //foreach (var e in employees.Where(e => e.AddressId > (employees.Count - 10)))
            //{
            //    Console.WriteLine(e.AddressText);
            //}
            //4-------------------------------------------------------------------------------------------------------------------------
            //var employeesProjects = db.Employees
            //        .Include(e => e.EmployeesProjects)
            //        .ThenInclude(e => e.Project)
            //        .Where(e => e.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
            //        .Take(30)
            //        .ToList();
            //
            //foreach (var e in employeesProjects)
            //{
            //    var managerId = e.ManagerId;
            //    var manager = db.Employees.Find(managerId);
            //
            //    Console.WriteLine($"{e.FirstName} {e.LastName} – Manager: {manager.FirstName} {manager.LastName}");
            //    foreach (var project in e.EmployeesProjects)
            //    {
            //        string format = "M/d/yyyy h:mm:ss tt";
            //
            //                string startDate = project.Project.StartDate.ToString(format, CultureInfo.InvariantCulture);
            //
            //                string endDate = project.Project.EndDate.ToString();
            //
            //                if (String.IsNullOrWhiteSpace(endDate))
            //                {
            //                    endDate = "not finished";
            //                }
            //                else
            //                {
            //                    endDate = project.Project.EndDate.Value.ToString(format, CultureInfo.InvariantCulture);
            //                }
            //
            //                Console.WriteLine($"--{project.Project.Name} - {startDate} - {endDate}");
            //    }
            //}
            //5-------------------------------------------------------------------------------------------------------------------------
            //var addresses = db.Addresses
            //            .Include(t => t.Town)
            //            .Select( a => new
            //            {
            //                a.AddressText,
            //                a.Town.Name,
            //                a.Employees.Count
            //            })
            //            .OrderBy(a => a.Name)
            //            .ThenBy(a => a.AddressText);
            //
            //foreach (var add in addresses.OrderByDescending(e => e.Count).Take(10))
            //{
            //    Console.WriteLine($"{add.AddressText}, {add.Name} - {add.Count} employees");
            //}
            //6-------------------------------------------------------------------------------------------------------------------------
            //var employee = db.Employees
            //        .Include(e => e.EmployeesProjects)
            //        .ThenInclude(e => e.Project)
            //        .SingleOrDefault(e => e.EmployeeId == 147);
            //
            //Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
            //foreach (var p in employee.EmployeesProjects.OrderBy(p => p.Project.Name))
            //{
            //    Console.WriteLine($"{p.Project.Name}");
            //}
            //7-------------------------------------------------------------------------------------------------------------------------
            //var dep = db.Departments
            //    .Include(e => e.Employees)
            //    .Select(d => new
            //    {
            //        d.Name,
            //        d.ManagerId,
            //        d.Employees
            //    })
            //    .Where(d => d.Employees.Count > 5)
            //    .OrderBy(e => e.Employees.Count)
            //    .ThenBy(d => d.Name)
            //    .ToList();
            //
            //foreach (var d in dep)
            //{
            //    var depManager = db.Employees.SingleOrDefault(m => m.EmployeeId == d.ManagerId);
            //
            //    Console.WriteLine($"{d.Name} - {depManager.FirstName} {depManager.LastName}");
            //    foreach (var emp in d.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
            //    {
            //        Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");
            //    }
            //    Console.WriteLine(new string('-', 10));
            //}
            //8-------------------------------------------------------------------------------------------------------------------------
            //var projects = db.Projects.OrderByDescending(p => p.StartDate).Take(10).ToList();
            //foreach (var p in projects.OrderBy(p => p.Name))
            //{
            //    Console.WriteLine($"{p.Name}");
            //    Console.WriteLine($"{p.Description}");
            //    var startDateFormat = "M/d/yyyy h:mm:ss tt";
            //    var dateString = p.StartDate.ToString(startDateFormat, CultureInfo.InvariantCulture);
            //    Console.WriteLine($"{dateString}");
            //}
            //9-------------------------------------------------------------------------------------------------------------------------
            //var employees = db.Employees
            //    .Where(e => e.DepartmentId == 1 || e.DepartmentId == 2 || e.DepartmentId == 4 || e.DepartmentId == 11).ToList();
            //foreach (var e in employees)
            //{
            //    e.Salary *= 1.12m;
            //}
            //db.SaveChanges();
            //foreach (var e in employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
            //{
            //    Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
            //}
            //10------------------------------------------------------------------------------------------------------------------------
            //var employees = db.Employees.Where(e => e.FirstName.Substring(0, 2) == "Sa").OrderBy(e => e.FirstName).ThenBy(e => e.LastName);
            //foreach (var e in employees)
            //{
            //    Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
            //}
            //14------------------------------------------------------------------------------------------------------------------------
            //var proj = db.Projects.Find(2);
            //var empProj = db.EmployeesProjects.Where(p => p.ProjectId == 2).ToList();
            //foreach (var ep in empProj)
            //{
            //    db.EmployeesProjects.Remove(ep);
            //}
            //db.Projects.Remove(proj);
            //db.SaveChanges();
            //var projects = db.Projects.Select(p => new { p.Name }).Take(10).ToList();
            //foreach (var p in projects)
            //{
            //    Console.WriteLine(p.Name);
            //}
        }
    }
}
