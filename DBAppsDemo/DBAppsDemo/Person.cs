 using System;
using System.Collections.Generic;
using System.Text;

namespace DBAppsDemo
{
    public class Person
    {
        public Person(string firstName, string lastName, string jobTitle)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.JobTitle = jobTitle;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public override string ToString()
        {
            var result = $"{this.FirstName} {this.LastName}";
            return result;
        }
    }
}
