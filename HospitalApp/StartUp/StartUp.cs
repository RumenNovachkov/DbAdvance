namespace HospitalStartUp
{
    using System;
    using HospitalApp.Data;
    using HospitalApp.Data.Models;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new HospitalDbContext())
            {
                db.Database.EnsureCreated();

                //var query = "SELECT * FROM Patients";
                
                var patCount = db.Patients.Count();
                
                Console.WriteLine(patCount);
            }
        }
    }
}
