namespace PhotoShare.Client
{
    using Core;
    using Data;
    using Models;
    using Core.Commands;
    using System;

    public class Application
    {
        public static void Main()
        {
            ResetDatabase();
            LogInCommandDispatcher logInCommandDispatcher = new LogInCommandDispatcher();
            LogInEngine logInEngine = new LogInEngine(logInCommandDispatcher);
            logInEngine.Run();
        }

        private static void ResetDatabase()
        {
            using (var db = new PhotoShareContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
