using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Worker : Human
{
    private decimal weeklySalary;
    private double workingHoursPerDay;
    private decimal salaryPerHour;

    public Worker(string firstName, string lastName, decimal weeklySalary, double workingHoursPerDay)
        :base(firstName, lastName)
    {
        this.WeeklySalary = weeklySalary;
        this.WorkingHoursPerDay = workingHoursPerDay;
    }

    public decimal WeeklySalary
    {
        get
        {
            return this.weeklySalary;
        }
        set
        {
            if (value <= 10)
            {
                throw new ArgumentException("Expected value mismatch! Argument: weekSalary");
            }
            else        this.weeklySalary = value;
        }
    }

    public double WorkingHoursPerDay
    {
        get
        {
            return this.workingHoursPerDay;
        }
        set
        {
            if (value < 1 || value > 12)
            {
                throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");
            }
            this.workingHoursPerDay = value;
        }
    }

    public decimal SalaryPerHour
    {
        get
        {
            return this.salaryPerHour;
        }
        set
        {
            this.salaryPerHour = value;
        }
    }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();

        result.AppendLine(base.ToString());
        result.AppendLine($"Week Salary: {this.WeeklySalary:F2}");
        result.AppendLine($"Hours per day: {this.WorkingHoursPerDay:F2}");
        
        decimal salaryPerHour = this.WeeklySalary / 5 / ((decimal)this.WorkingHoursPerDay);

        result.Append($"Salary per hour: {salaryPerHour:F2}");

        return result.ToString();
    }
}
