namespace TeamBuilder.App
{
    using Core;
    using Data;

    public class StartUp
    {
        public static void Main()
        {
            //ResetDatabase();
            LogInCommandDispatcher logInCommandDispatcher = new LogInCommandDispatcher();
            LogInEngine logInEngine = new LogInEngine(logInCommandDispatcher);
            logInEngine.Run();
        }

        private static void ResetDatabase()
        {
            using (var db = new TeamBuilderContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
