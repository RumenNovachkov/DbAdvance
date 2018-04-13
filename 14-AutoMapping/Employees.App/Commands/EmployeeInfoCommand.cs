namespace Employees.App.Commands
{
    using Employees.Services;
    using System;

    class EmployeeInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public EmployeeInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);

            var employee = employeeService.ById(employeeId);

            if (employee == null)
            {
                throw new ArgumentException($"There is no Employee with ID: {employeeId}");
            }

            return $"ID: {employee.Id} - {employee.FirstName} {employee.LastName} - ${employee.Salary:f2}";
        }
    }
}
