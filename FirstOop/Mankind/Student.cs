using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Student : Human
{
    private string facultyNumber;

    public Student(string firstName, string lastName, string facilityNumber)
        :base(firstName, lastName)
    {
        this.FacultyNumber = facilityNumber;
    }

    public string FacultyNumber
    {
        get
        {
            return this.facultyNumber;
        }
        set
        {
            if (value.Length < 5 || value.Length > 10 || value.Any(x => !char.IsLetterOrDigit(x)))
            {
                throw new ArgumentException("Invalid faculty number!");
            }
            this.facultyNumber = value;
        }
    }


    public override string ToString()
    {
        StringBuilder result = new StringBuilder();

        result.AppendLine(base.ToString());
        result.Append($"Faculty number: {this.FacultyNumber}");

        return result.ToString();
    }
}
