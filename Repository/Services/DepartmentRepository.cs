using Microsoft.EntityFrameworkCore;
using practice_dotnet.Data;
using practice_dotnet.Models.Entities;
using practice_dotnet.Repository.Interface;

namespace practice_dotnet.Repository.Services
{
	public class DepartmentRepository: IDepartmentRepository
	{
		private readonly ApplicationDbContext dbContext;

		public DepartmentRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<IEnumerable<Department>> GetAllDepartments()
		{
			return await dbContext.Department.ToListAsync();
		}

		public async Task<Department> AddDepartment(Department department)
		{
			var newDepartment = new Department()
			{
				DepartmentName = department.DepartmentName,
			};

			await dbContext.Department.AddAsync(newDepartment);
			await dbContext.SaveChangesAsync();

			return newDepartment;
		}

		public async Task<Department> UpdateDepartment(int id, Department department)
		{
			var foundDepartment = await dbContext.Department.FindAsync(id);
			if (foundDepartment == null)
			{
				throw new KeyNotFoundException($"Employee with ID '{id}' was not found.");
			}

			foundDepartment.DepartmentId = id;
			foundDepartment.DepartmentName = department.DepartmentName;

			await dbContext.Department.AddAsync(foundDepartment);
			await dbContext.SaveChangesAsync();

			return foundDepartment;
		}

		public async Task<Department> GetDepartmentById(int id)
		{
			var foundDepartment = await dbContext.Department.FindAsync(id);
			if (foundDepartment == null)
			{
				throw new KeyNotFoundException($"Employee with ID '{id}' was not found.");
			}

			return foundDepartment;
		}

		public async Task<bool> DeleteDepartment(int id)
		{
			var foundDepartment = await dbContext.Department.FindAsync(id);
			if (foundDepartment == null)
			{
				return false;
			}

			foundDepartment.DeletedAt = DateTime.UtcNow;

			await dbContext.SaveChangesAsync();

			return true;
		}
	}
}
