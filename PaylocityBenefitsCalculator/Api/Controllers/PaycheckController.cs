using Data.Dtos.Dependent;
using Data.Dtos.Employee;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
	public class PaycheckController : Controller
	{
		private readonly IPayService _payService;
		private readonly IEmployeeService _employeeService;
		public PaycheckController(IPayService payService, IEmployeeService employeeService)
		{
			_payService = payService;
			_employeeService = employeeService;
		}

		[SwaggerOperation(Summary = "Get single paycheck by an employee id")]
		[HttpGet("{employeeId}")]
		public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int employeeId)
		{
			// check for employee existing
			var employee = await _employeeService.GetAsyncEmployeeById(employeeId);
			if (employee == null)
			{
				return NotFound("Employee with ID " + employeeId + " not found");
			}
			var paycheck = await _payService.GetPaycheck(employee);
			paycheck = await _payService.RoundPayCheckInfo(paycheck);
			
			return Ok(paycheck);
		}
	}
}
