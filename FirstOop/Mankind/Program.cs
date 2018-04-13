using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static void Main(string[] args)
    {
        var studentArgs = Console.ReadLine().Split();
        var firstNameS = studentArgs[0];
        var lastNameS = studentArgs[1];
        var facNumberS = studentArgs[2];

        var workerArgs = Console.ReadLine().Split();
        var firstNameW = workerArgs[0];
        var lastNameW = workerArgs[1];
        var weeklySalaryW = decimal.Parse(workerArgs[2]);
        var workingHoursW = double.Parse(workerArgs[3]);

        try
        {
            var student = new Student(firstNameS, lastNameS, facNumberS);
            Console.WriteLine(student.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }

        Console.WriteLine();

        try
        {
            var worker = new Worker(firstNameW, lastNameW, weeklySalaryW, workingHoursW);
            Console.WriteLine(worker.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
    }
}
