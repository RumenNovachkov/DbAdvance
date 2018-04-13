namespace Employees.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Employees.Data;
    using Employees.DtoModels;
    using Employees.Models;
    using Employees.Services.Contracts;

    using Microsoft.EntityFrameworkCore;


    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeesContext context;

        public EmployeeService(EmployeesContext context)
        {
            this.context = context;
        }

        public EmployeeDto ById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException($"There is no Employee with ID: {employeeId}");
            }

            var employeeDto = Mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public EmployeePersonalDto PersonalById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException($"There is no Employee with ID: {employeeId}");
            }

            var employeeDto = Mapper.Map<EmployeePersonalDto>(employee);

            return employeeDto;
        }

        public void AddEmployee(EmployeeDto employeeDto)
        {
            var employee = Mapper.Map<Employee>(employeeDto);

            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public string SetBirthday(int employeeId, DateTime date)
        {
            var employee = context.Employees
                .Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException($"There is no Employee with ID: {employeeId}");
            }

            employee.Birthday = date;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public string SetAddress(int employeeId, string address)
        {
            var employee = context.Employees
                .Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException($"There is no Employee with ID: {employeeId}");
            }

            employee.Address = address;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public EmployeePersonalDto SetManager(int employeeId, int managerId)
        {
            var employee = context.Employees
                .Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException($"There is no Employee with ID: {employeeId}");
            }

            var manager = context.Employees
                .Find(managerId);

            employee.Manager = manager;

            context.SaveChanges();

            var employeePersonalDto = Mapper.Map<EmployeePersonalDto>(employee);

            return employeePersonalDto;
        }

        public ManagerDto GetManager(int managerId)
        {
            var employee = context.Employees
                .Include(m => m.ManagerEmployees)
                .SingleOrDefault(m => m.Id == managerId);

            var managerDto = Mapper.Map<ManagerDto>(employee);

            return managerDto;
        }

        public List<EmployeeManagerDto> OlderThan(int age)
        {
            var employees = context.Employees
                .Where(e => e.Birthday != null && Math.Floor((DateTime.Now - e.Birthday.Value).TotalDays / 365) > age)
                .Include(e => e.Manager)
                .ProjectTo<EmployeeManagerDto>()
                .ToList();

            return employees;
        }
    }
}
