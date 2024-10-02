using Microsoft.EntityFrameworkCore;
using practice_dotnet.Models.Entities;

namespace practice_dotnet.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

        public DbSet<Employee> Employees { get; set; }
    }
}
