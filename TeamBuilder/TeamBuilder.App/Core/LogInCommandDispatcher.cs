namespace TeamBuilder.App.Core
{
    using TeamBuilder.Models;
    using System;

    public class LogInCommandDispatcher
    {
        public User DispatchCommand(string[] commandParameters)
        {
            string command = commandParameters[0].ToLower();

            User user = new User();

            switch (command)
            {
                default:
                    break;
            }

            return user;
        }
    }
}
