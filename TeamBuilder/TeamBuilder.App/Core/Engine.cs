namespace TeamBuilder.App.Core
{
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    using System;
    using System.Linq;

    public class Engine
    {
        private readonly CommandDispatcher commandDispatcher;

        public Engine(CommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public void Run(User loggedUser)
        {
            while (true)
            {
                try
                {
                    User user = loggedUser;
                    using (var db = new TeamBuilderContext())
                    {
                        var currentUser = db.Users.SingleOrDefault(u => u.Id == user.Id);

                        string input = Console.ReadLine().Trim();
                        string[] data = input.Split(' ');
                        string result = this.commandDispatcher.DispatchCommand(data, currentUser);
                        if (result == "DeletedUser")
                        {
                            Console.WriteLine("We are sorry to tell You Good-Bye. Your profile is now deleted." + Environment.NewLine
                                + "You may come back any time. We hope to see You again.");
                            return;
                        }
                        if (result == "Logout")
                        {
                            Console.WriteLine("You are now logged out! Good-Bye.");
                            return;
                        }
                        Console.WriteLine(result);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
