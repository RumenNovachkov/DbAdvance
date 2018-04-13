using System;
using System.Data.SqlClient;

namespace FirstDbAdvanceProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new SqlConnectionStringBuilder()
            {
                ["Server"] = "RZR\\SQLEXPRESS", 
                ["Integrated Security"] = "True"
            };
            var connection = new SqlConnection(builder.ToString());

            connection.Open();

            using (connection)
            {
                try
                {
                    var createDbQuery = "CREATE DATABASE MinionsDB";
                    ExecuteCommand(createDbQuery, connection);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            builder.Add("DATABASE", "MinionsDB");
            connection = new SqlConnection(builder.ToString());
            connection.Open();
            using (connection)
            {
                try
                {
                    string cTCCountries = "CREATE TABLE Countries ( Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(50))";
                    string cTCTowns = "CREATE TABLE Towns ( Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(50), CountryId INT NOT NULL, CONSTRAINT FK_TownCountry FOREIGN KEY (CountryId) REFERENCES Countries(Id))";
                    string cTCEvilnessFactors = "CREATE TABLE EvilnessFactors ( Id INT PRIMARY KEY,	Name NVARCHAR(10) UNIQUE NOT NULL )";
                    string cTCVillains = "CREATE TABLE Villains (	Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(50), EvilnessFactorId INT CONSTRAINT FK_VillainEvilnessFactor FOREIGN KEY (EvilnessFactorId) REFERENCES EvilnessFactors(Id) )";
                    string cTCMinions = "CREATE TABLE Minions ( Id INT PRIMARY KEY IDENTITY,	Name NVARCHAR(50), Age INT,	TownId INT, CONSTRAINT FK_Towns FOREIGN KEY (TownId) REFERENCES Towns(Id) )";
                    string cTCMinionsVillains = "CREATE TABLE MinionsVillains ( MinionId INT,	VillainId INT, CONSTRAINT FK_Minions FOREIGN KEY (MinionId) REFERENCES Minions(Id), CONSTRAINT FK_Villains FOREIGN KEY (VillainId) REFERENCES Villains(Id) )";

                    ExecuteCommand(cTCCountries, connection);
                    ExecuteCommand(cTCTowns, connection);
                    ExecuteCommand(cTCEvilnessFactors, connection);
                    ExecuteCommand(cTCVillains, connection);
                    ExecuteCommand(cTCMinions, connection);
                    ExecuteCommand(cTCMinionsVillains, connection);
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
