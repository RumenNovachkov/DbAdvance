namespace Employees.Services
{
    using Employees.Data;
    using Employees.Services.Contracts;

    using Microsoft.EntityFrameworkCore;

    public class DatabaseInitializerService : IDatabaseInitializerService
    {
        private readonly EmployeesContext context;

        public DatabaseInitializerService(EmployeesContext context)
        {
            this.context = context;
        }

        public void InitializeDatabase()
        {
            this.context.Database.Migrate();
        }
    }
}
