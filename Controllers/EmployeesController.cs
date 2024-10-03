using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practice_dotnet.Data;
using practice_dotnet.Models.DTO;
using practice_dotnet.Models.Entities;
using practice_dotnet.Repository.Interface;
using practice_dotnet.utils;

namespace practice_dotnet.Controllers
{
    //localhost:xxxx/api/employees
    [Route("api/[controller]")]
	[ApiController]
	public class EmployeesController : ControllerBase
	{
		private readonly IEmployeeRepository employeeRepository;
		public EmployeesController(IEmployeeRepository employeeRepository)
		{
			this.employeeRepository = employeeRepository;
		}

		[HttpGet]
		public async Task<ActionResult> GetAllEmployees()
		{
			try
			{
				return Ok(await employeeRepository.GetAllEmployees());
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
				 "Error retrieving data from the database");
			}
		}

		[HttpGet]
		[Route("{id:guid}")]
		public async Task<ActionResult> GetEmployeeById(Guid id)
		{
			try
			{
				return Ok(await employeeRepository.GetEmployeeById(id));
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
				 "Error retrieving data from the database");
			}

		}
		[HttpPost]
		public async Task<ActionResult> AddEmployee(AddEmployeeDto addEmployeeDto)
		{
			var employeeEntity = new Employee
			{
				Email = addEmployeeDto.Email,
				Name = addEmployeeDto.Name,
				Salary = addEmployeeDto.Salary,
				Phone = addEmployeeDto.Phone,
			};

			try
			{
				return Ok(await employeeRepository.AddEmployee(employeeEntity));
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
				 "Error retrieving data from the database");
			}
		}

		[HttpPut]
		[Route("{id:guid}")]
		public async Task<ActionResult> UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployee)
		{
			var employeeEntity = new Employee
			{
				Email = updateEmployee.Email,
				Name = updateEmployee.Name,
				Salary = updateEmployee.Salary,
				Phone = updateEmployee.Phone,
			};

			try
			{
				return Ok(await employeeRepository.UpdateEmployee(id, employeeEntity));
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
				 "Error retrieving data from the database");
			}
		}

		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> DeleteEmployee(Guid id)
		{
			try
			{
				return Ok(await employeeRepository.DeleteEmployee(id));
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error deleting data from the database");
			}
		}
	}
}
