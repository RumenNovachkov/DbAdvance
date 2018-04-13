namespace P01_HospitalDatabase
{
    using System;
    using P01_HospitalDatabase.Data;
    using P01_HospitalDatabase.Initializer;
    using P01_HospitalDatabase.Data.Models;

    public class StartUp
    {
        static void Main(string[] args)
        {
            //DatabaseInitializer.ResetDatabase();

            using (var db = new HospitalContext())
            {
                DatabaseInitializer.SeedPatients(db, 100);
            }
        }
    }
}
