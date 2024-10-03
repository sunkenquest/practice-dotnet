using Microsoft.AspNetCore.Mvc;
using practice_dotnet.Models.DTO.Department;
using practice_dotnet.Models.Entities;
using practice_dotnet.Repository.Interface;

namespace practice_dotnet.Controllers
{
	//localhost:xxxx/api/department
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController: ControllerBase
	{
		private readonly IDepartmentRepository departmentRepository;
		public DepartmentController(IDepartmentRepository departmentRepository)
		{
			this.departmentRepository = departmentRepository;
		}

		[HttpGet]
		public async Task<ActionResult> GetAlLDepartment()
		{
			try
			{
				return Ok(await departmentRepository.GetAllDepartments());
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
				 "Error retrieving data from the database");
			}
		}

		[HttpPost]
		public async Task<ActionResult> AddDeparment(AddDepartmentDto addDepartmentDto)
		{
			var departmentEntity = new Department
			{
				DepartmentName = addDepartmentDto.DepartmentName,
			};

			try
			{
				return Ok(await departmentRepository.AddDepartment(departmentEntity));
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
				 "Error retrieving data from the database");
			}
		}

		[HttpPut]
		[Route("{id:int}")]
		public async Task<ActionResult> UpdateDepartment(int id, UpdateDepartmentDto updateDepartmentDto)
		{
			var departmentEntity = new Department
			{
				DepartmentId = updateDepartmentDto.DepartmentId,
				DepartmentName = updateDepartmentDto.DepartmentName,
			};

			try
			{
				return Ok(await departmentRepository.UpdateDepartment(id, departmentEntity));
			}

			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
				 "Error retrieving data from the database");
			}
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<ActionResult> GetDepartment(int id)
		{
			try
			{
				return Ok(await departmentRepository.GetDepartmentById(id));
			}

			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
				 "Error retrieving data from the database");
			}
		}

		[HttpDelete]
		[Route("{id:int}")]
		public async Task<ActionResult> DeleteDepartment(int id)
		{
			try
			{
				return Ok(await departmentRepository.DeleteDepartment(id));
			}

			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
				 "Error retrieving data from the database");
			}
		}
	}
}
