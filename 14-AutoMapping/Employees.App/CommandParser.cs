namespace Employees.App
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Employees.App.Commands;

    public class CommandParser
    {
        private readonly IServiceProvider serviceProvider;

        public CommandParser(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommand ParseCommand(string commandName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
                .ToArray();

            var commandType = commandTypes.SingleOrDefault(t => t.Name.ToLower() == $"{commandName.ToLower()}command");

            if(commandType == null)
            {
                throw new InvalidOperationException("Invalid command");
            }

            var constructor = commandType.GetConstructors().First();

            var constructorParamsTypes= constructor
                .GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();

            var services = constructorParamsTypes
                .Select(serviceProvider.GetService)
                .ToArray();

            var command = (ICommand) constructor.Invoke(services);

            return command;
        }
    }
}
