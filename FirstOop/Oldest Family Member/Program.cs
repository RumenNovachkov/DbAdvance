using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static void Main()
    {
        var drd = new List<Person>();
        var count = int.Parse(Console.ReadLine());
        for (int i = 0; i < count; i++)
        {
            var personArgs = Console.ReadLine().Split();
            var name = personArgs[0];
            var age = int.Parse(personArgs[1]);
            var rdr = new Person(name, age);
            drd.Add(rdr);
        }

        string highestAgePerson = string.Empty;
        int highestAge = 0;

        foreach (var p in drd)
        {
            if (p.Age > highestAge)
            {
                highestAge = p.Age;
                highestAgePerson = p.Name;
            }
        }

        Console.WriteLine($"{highestAgePerson} {highestAge}");
    }
}
