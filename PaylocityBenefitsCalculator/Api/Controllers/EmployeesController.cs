using Data.Dtos.Dependent;
using Data.Dtos.Employee;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private IEmployeeService _employeeService;
	public EmployeesController(IEmployeeService employeeService)
	{
		_employeeService = employeeService;
	}

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var employee = await _employeeService.GetAsyncEmployeeById(id);
        
        if(employee == null)
        {
            return NotFound("Employee with ID " + id + " not found");
        }

        return Ok(employee);
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: use a more realistic production approach
        var employees = await _employeeService.GetAsyncEmployees();
        return Ok(employees);
    }
}
