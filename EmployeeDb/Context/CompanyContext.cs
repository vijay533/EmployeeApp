using EmployeeDb.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDb.Context
{
    public class CompanyContext :DbContext
    {
        public CompanyContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Employee> employees { get; set; }
    }
}
