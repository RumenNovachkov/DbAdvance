using PhotoShare.Client.Core.LogInCommands;
using PhotoShare.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Client.Core
{
    public class LogInCommandDispatcher
    {
        public User DispatchCommand(string[] commandParameters)
        {
            string command = commandParameters[0].ToLower();

            User user = new User();

            switch (command)
            {
                case "return":
                    user = ReturnCommand.Execute(commandParameters);
                    Console.WriteLine("Welcome back");
                    break;
                case "login":
                    user = LogInCommand.Execute(commandParameters, user);
                    break;
                case "registeruser":
                    user = RegisterUserCommand.Execute(commandParameters);
                    break;
                default:
                    throw new InvalidOperationException($"LogInCommand {command} not valid!");
                    break;
            }

            return user;
        }
    }
}
