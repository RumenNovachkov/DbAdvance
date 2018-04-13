using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace VillainNames
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new SqlConnectionStringBuilder()
            {
                ["Server"] = "RZR\\SQLEXPRESS",
                ["DataBase"] = "MinionsDB",
                ["Integrated Security"] = "True"
            };
            var connection = new SqlConnection(builder.ToString());
            connection = new SqlConnection(builder.ToString());
            connection.Open();
            var countOfMinions = new List<VillainNamesAndMinionsCount>();
            using (connection)
            {
                try
                {
                    string VillainMinionsCount = "SELECT v.Name AS Name, COUNT(m.Name) AS CountM FROM Villains AS v JOIN MinionsVillains AS mv ON v.Id = mv.VillainId JOIN Minions AS m ON mv.MinionId = m.Id GROUP BY v.Name";

                    var newCommand = new SqlCommand(VillainMinionsCount, connection);

                    var reader = newCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        var villainName = (string)reader["Name"];
                        var minionsCount = (int)reader["CountM"];
                        var singleVillain = new VillainNamesAndMinionsCount(villainName, minionsCount);
                        countOfMinions.Add(singleVillain);
                    }

                    foreach (var v in countOfMinions.Where(v => v.MinionsCount > 3).OrderByDescending(v => v.MinionsCount))
                    {
                        Console.WriteLine($"{v.Name} - {v.MinionsCount}");
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
        }

        static void ExecuteCommand(string command, SqlConnection connection)
        {
            var sql = new SqlCommand(command, connection);
            sql.ExecuteNonQuery();
        }
    }
}
