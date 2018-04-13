namespace Employees.App.Commands
{
    using System;
    using System.Globalization;

    using Employees.Services;

    class SetBirthdayCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public SetBirthdayCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //•	SetBirthday <employeeId> <date: "dd-MM-yyyy"> 
        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);
            DateTime birthday = DateTime.ParseExact(args[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            string employeeName = employeeService.SetBirthday(employeeId, birthday);

            return $"{employeeName}'s birthday was set to {args[1]}.";
        }
    }
}
