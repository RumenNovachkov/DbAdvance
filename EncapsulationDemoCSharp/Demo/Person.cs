using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Person
{
    private const int minimumNameLength = 3;
    private const double minimumSalaryValue = 460.00;
    private string firstName;
    private string lastName;
    private int age;
    private double salary;

    public Person(string firstName, string lastName, int age, double salary)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Age = age;
        this.Salary = salary;
    }

    public string FirstName
    {
        get
        {
            return firstName;
        }
        set
        {
            if (value.Length <= minimumNameLength)
            {
                throw new ArgumentException($"First name cannot be less than {minimumNameLength} symbols");
            }
            this.firstName = value;
        }
    }

    public string LastName
    {
        get
        {
            return lastName;
        }
        set
        {
            if (value.Length <= minimumNameLength)
            {
                throw new ArgumentException($"Last name cannot be less than {minimumNameLength} symbols");
            }
            this.lastName = value;
        }
    }

    public int Age
    {
        get
        {
            return age;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Age cannot be zero or negative integer");
            }
            this.age = value;
        }
    }

    public double Salary
    {
        get
        {
            return this.salary;
        }
        private set
        {
            if (value < minimumSalaryValue)
            {
                throw new ArgumentException($"Salary cannot be less than {minimumSalaryValue} leva");
            }
            this.salary = value;
        }

    }

    public void IncreaseSalary(double percent)
    {
        if (this.age > 30)
        {
            this.salary += this.salary * percent / 100;
        }
        else
        {
            this.salary += this.salary * percent / 200;
        }
    }

    public string FullName()
    {
        return this.FirstName + " " + this.LastName;
    }

    public override string ToString()
    {
        return $"{this.firstName} {this.lastName} get {this.salary:f2} leva";
    }
}
