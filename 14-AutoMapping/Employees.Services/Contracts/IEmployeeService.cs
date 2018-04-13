namespace Employees.Services.Contracts
{
    using System;
    using Employees.DtoModels;

    public interface IEmployeeService
    {
        EmployeeDto ById(int employeeId);
        void AddEmployee(EmployeeDto employee);
        string SetBirthday(int employeeId, DateTime date);
        string SetAddress(int employeeId, string address);
    }
}
