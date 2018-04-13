namespace Employees.App
{
    using System;
    using System.Linq;

    using Employees.Services.Contracts;

    using Microsoft.Extensions.DependencyInjection;

    internal class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        
        internal void Run()
        {
            var databaseInitializer = serviceProvider.GetService<IDatabaseInitializerService>();
            databaseInitializer.InitializeDatabase();

            var commandParser = new CommandParser(serviceProvider);

            while (true)
            {
                string input = Console.ReadLine();
                string[] commandTokens = input.Split();

                string commandName = commandTokens[0];
                string[] commandArgs = commandTokens.Skip(1).ToArray();

                try
                {
                    var command = commandParser.ParseCommand(commandName);

                    var result = command.Execute(commandArgs);

                    Console.WriteLine(result);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
