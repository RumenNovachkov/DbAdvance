using P01_HospitalDatabase;

namespace HospitalStartUp
{
    using System;
    using P01_HospitalDatabase.Data;
    using P01_HospitalDatabase.Initializer;
    using P01_HospitalDatabase.Data.Models;

    class StartUp
    {
        static void Main(string[] args)
        { 
            using (var db = new HospitalContext())
            {
                DatabaseInitializer.SeedPatients(db, 100);
            }
        }
    }
}
