using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static void Main()
    {
        var count = int.Parse(Console.ReadLine());

        var departments = new Dictionary<string, List<Employee>>();
        for (int i = 0; i < count; i++)
        {
            var input = Console.ReadLine().Split();
            var name = input[0];
            var salary = decimal.Parse(input[1]);
            var position = input[2];
            var department = input[3];
            var emp = new Employee(name, salary, position, department);
            
            if (input.Length == 5)
            {
                var isAge = int.TryParse(input[4], out int age);
                if (isAge)
                {
                    emp.Age = age;
                }
                else
                {
                    emp.Email = input[4];
                }
            }
            else if (input.Length == 6)
            {
                emp.Email = input[4];
                emp.Age = int.Parse(input[5]);
            }
            if (!departments.ContainsKey(department))
            {
                departments.Add(department, new List<Employee>());
            }
            departments[department].Add(emp);
        }

        decimal highestAvgSalary = 0;
        string highestSalaryDep = "";
        foreach (var d in departments)
        {
            decimal depSalary = 0;
            foreach (var e in d.Value)
            {
                depSalary += e.Salary;
            }
            decimal avgSalary = depSalary / d.Value.Count();

            if (avgSalary > highestAvgSalary)
            {
                highestAvgSalary = avgSalary;
                highestSalaryDep = d.Key;
            }
        }

        Console.WriteLine($"Highest Average Salary: {highestSalaryDep}");
        foreach (var e in departments[highestSalaryDep].OrderByDescending(e => e.Salary))
        {
            Console.WriteLine($"{e.Name} {e.Salary:f2} {e.Email} {e.Age}");
        }
    }
}
