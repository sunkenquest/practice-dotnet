using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using practice_dotnet.Models.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace practice_dotnet.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Department> Department { get; set; }

		// Handle soft-deleted records not being included in query results
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);  // Must call the base method for Identity to work properly

			modelBuilder.Entity<Employee>().HasQueryFilter(e => e.DeletedAt == null);

			// You can add more query filters here if necessary
		}

		// Handle auto update of timestamps
		public override int SaveChanges()
		{
			HandleTimestamps();
			return base.SaveChanges();
		}

		// Handle auto update of timestamps (async)
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			HandleTimestamps();
			return base.SaveChangesAsync(cancellationToken);
		}

		private void HandleTimestamps()
		{
			var entries = ChangeTracker.Entries()
				.Where(e => e.Entity != null &&
							(e.State == EntityState.Added || e.State == EntityState.Modified));

			foreach (var entry in entries)
			{
				var entity = entry.Entity;

				var createdAtProp = entity.GetType().GetProperty("CreatedAt");
				if (createdAtProp != null && entry.State == EntityState.Added)
				{
					createdAtProp.SetValue(entity, DateTime.Now);
				}

				var updatedAtProp = entity.GetType().GetProperty("UpdatedAt");
				if (updatedAtProp != null && (entry.State == EntityState.Modified || entry.State == EntityState.Added))
				{
					updatedAtProp.SetValue(entity, DateTime.Now);
				}
			}
		}
	}
}
