namespace Employees.App.Commands
{
    using System;

    using Employees.Services;
    using System.Text;

    class EmployeePersonalInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public EmployeePersonalInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);

            var employee = employeeService.PersonalById(employeeId);
            
            string birthday = employee.Birthday != null ? employee.Birthday.Value.ToString("dd-MM-yyyy") : "[no birthday specified]";

            string address = employee.Address ?? "[no address specified]";

            var strBuilder = new StringBuilder();

            strBuilder.Append($"ID: {employee.Id} - {employee.FirstName} {employee.LastName} - ${employee.Salary:F2}" + Environment.NewLine);
            strBuilder.Append($"Birthday: {birthday}" + Environment.NewLine);
            strBuilder.Append($"Address: {address}");

            return strBuilder.ToString();
        }
    }
}
