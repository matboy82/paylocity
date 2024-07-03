using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dtos.Employee;
namespace Service.Services
{
	public interface IEmployeeService
	{

		Task<List<GetEmployeeDto>> GetAsyncEmployees();

		Task<GetEmployeeDto> GetAsyncEmployeeById(int id);

		Task<GetEmployeeDto> GetAsyncEmployeeDtoByName(string name);
		Task<bool> AddAsyncEmployee(GetEmployeeDto employee);

		Task<bool> UpdateAsyncEmployee(GetEmployeeDto employee);

		Task<bool> DeleteAsyncEmployee(int id);

	}
}
