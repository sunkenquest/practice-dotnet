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
		public DbSet<Department> Department { get; set; }


		//handle soft deleted records not be included in query
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Employee>().HasQueryFilter(e => e.DeletedAt == null);
		}

		// handle auto update of timestamps
		public override int SaveChanges()
		{
			HandleTimestamps();
			return base.SaveChanges();
		}

		// handle auto update of timestamps
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
				if (updatedAtProp != null && entry.State == EntityState.Modified)
				{
					updatedAtProp.SetValue(entity, DateTime.Now);
				}
			}
		}

	}
}
