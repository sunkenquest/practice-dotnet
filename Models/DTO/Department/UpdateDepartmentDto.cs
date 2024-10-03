namespace practice_dotnet.Models.DTO.Department
{
	public class UpdateDepartmentDto
	{
		public int DepartmentId { get; set; }
		public required string DepartmentName { get; set; }
	}
}
