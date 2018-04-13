﻿using PhotoShare.Data;
using PhotoShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoShare.Client.Core
{
    public class LogInEngine
    {
        private readonly LogInCommandDispatcher logInCommandDispatcher;

        public LogInEngine(LogInCommandDispatcher logInCommandDispatcher)
        {
            this.logInCommandDispatcher = logInCommandDispatcher;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("LogIn or Register new User");
                    string input = Console.ReadLine().Trim();
                    string[] data = input.Split(' ');
                    if (data[0].ToLower() == "exit")
                    {
                        Console.WriteLine("Bye-bye!");
                        Environment.Exit(0);
                        return;
                    }

                    User loggedUser = this.logInCommandDispatcher.DispatchCommand(data);
                    using (var db = new PhotoShareContext())
                    {
                        var currentUser = db.Users.SingleOrDefault(u => u.Id == loggedUser.Id);
                        if (currentUser.IsDeleted == true)
                        {
                            throw new InvalidOperationException($"{currentUser.Username} is Deleted, You need to return. Type: \"Return <Username> <Password>\"");
                        }
                    }
                    CommandDispatcher commandDispatcher = new CommandDispatcher();
                    
                    Engine engine = new Engine(commandDispatcher);
                    engine.Run(loggedUser);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
