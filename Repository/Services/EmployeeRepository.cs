using practice_dotnet.Models.Entities;
using System;
using practice_dotnet.Repository.Interface;
using practice_dotnet.Data;
using Microsoft.EntityFrameworkCore;

namespace practice_dotnet.Repository.Services
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly ApplicationDbContext dbContext;

		public EmployeeRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<IEnumerable<Employee>> GetAllEmployees()
		{
			return await dbContext.Employees.ToListAsync();
		}

		public async Task<Employee> GetEmployeeById(Guid employeeId)
		{
			var employee = await dbContext.Employees
				.FirstOrDefaultAsync(e => e.Id == employeeId);

			if (employee == null)
			{
				throw new KeyNotFoundException($"Employee with ID '{employeeId}' was not found.");
			}

			return employee;
		}

		public async Task<Employee> AddEmployee(Employee employee)
		{
			var newEmployee = new Employee()
			{
				Name = employee.Name,
				Email = employee.Email,
				Phone = employee.Phone,
				Salary = employee.Salary,
			};

			await dbContext.Employees.AddAsync(newEmployee);
			await dbContext.SaveChangesAsync();

			return newEmployee;
		}

		public async Task<Employee> UpdateEmployee(Guid id, Employee employee)
		{
			var foundEmployee = await dbContext.Employees.FindAsync(id);
			if (foundEmployee == null)
			{
				throw new KeyNotFoundException($"Employee with ID '{id}' was not found.");
			}

			foundEmployee.Name = employee.Name;
			foundEmployee.Email = employee.Email;
			foundEmployee.Phone = employee.Phone;
			foundEmployee.Salary = employee.Salary;

			await dbContext.SaveChangesAsync();

			return foundEmployee;
		}

		public async Task<bool> DeleteEmployee(Guid id)
		{
			var foundEmployee = await dbContext.Employees.FindAsync(id);
			if (foundEmployee == null)
			{
				return false;
			}

			foundEmployee.DeletedAt = DateTime.UtcNow;

			await dbContext.SaveChangesAsync();

			return true;
		}
	}
}
