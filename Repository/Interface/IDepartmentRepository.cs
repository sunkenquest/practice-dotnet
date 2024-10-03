using practice_dotnet.Models.Entities;

namespace practice_dotnet.Repository.Interface
{
	public interface IDepartmentRepository
	{
		Task<IEnumerable<Department>> GetAllDepartments();
		Task<Department> AddDepartment(Department department);
		Task<Department> UpdateDepartment(int id, Department department);
		Task<Department> GetDepartmentById(int id);
		Task<bool> DeleteDepartment(int id);



	}
}
