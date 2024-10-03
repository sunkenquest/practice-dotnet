using practice_dotnet.Models.Entities;

namespace practice_dotnet.Repository.Interface
{
	public interface IEmployeeRepository
	{
		Task<IEnumerable<Employee>> GetAllEmployees();
		Task<Employee> GetEmployeeById(Guid employeeId);
		Task<Employee> AddEmployee(Employee employee);
		Task<Employee> UpdateEmployee(Guid id,Employee employee);
		Task<bool> DeleteEmployee(Guid id);
	}
}
