namespace practice_dotnet.Models.Entities
{
	public class Department
	{
		public int DepartmentId { get; set; }
		public required string DepartmentName { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
	}
}
