using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DBAppsDemo
{
    class Program
    {
        public static void Main(string[] args)
        {
            var connection = new SqlConnection
                ("Server=RNOVACHKOVPC\\SQLEXPRESS;" +
                 "Database=SoftUni;" +
                 "Integrated Security=True"
                );
           
            connection.Open();

            using (connection)
            {
                var command = new SqlCommand("SELECT FirstName, LastName, JobTitle FROM Employees", connection);

                var people = new List<Person>();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var firstName = (string)reader["FirstName"];
                    var lastName = (string)reader["LastName"];
                    var jobTitle = (string)reader["JobTitle"];
                    var person = new Person(firstName, lastName, jobTitle);
                    people.Add(person);
                }

                var groupedPeople = people.GroupBy(p => p.JobTitle)
                                          .OrderByDescending(g => g.Count())
                                          .ToList();

                foreach (var group in groupedPeople)
                {
                    Console.WriteLine($":{group.Key}: >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                    foreach (var person in group.OrderBy(p => p.FirstName).ThenBy(p => p.LastName))
                    {
                        Console.WriteLine(person);
                    }
                }


                //var transaction = connection.BeginTransaction();
                //
                //var command = new SqlCommand(
                //    $"DELETE FROM Towns WHERE Name = ('{town}')", connection, transaction);
                //var rowsAffected = command.ExecuteNonQuery();
                //transaction.Rollback();
                //Console.WriteLine("Rows affected: " + rowsAffected);
            }
        }
    }
}
