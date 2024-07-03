using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data.Dtos.Employee;
using Data.Models;
using Data.Repositories;

namespace Service.Services
{
	/// <summary>
	/// Interface implementation for employee service. This is where any business logic for the service lives.
	/// </summary>
	public class EmployeeService : IEmployeeService
	{
		private readonly IRepository<GetEmployeeDto> _employeeRepository;
		public EmployeeService(IRepository<GetEmployeeDto> employeeRepository) 
		{ 
			_employeeRepository = employeeRepository;
		}

		/// <summary>
		/// Updates an employee in the database if employee meets business rules
		/// </summary>
		/// <param name="employee"></param>
		/// <returns></returns>
		public Task<bool> UpdateAsyncEmployee(GetEmployeeDto employee)
		{
			// Employee can have either a spouse OR domestic partner, not both or multiple
			if (CheckEmployeeValid(employee))
			{
				return _employeeRepository.UpdateAsync(employee);
			}
			return Task.FromResult(false); // invalid, can't have both spouse/partner
		}

		public Task<bool> AddAsyncEmployee(GetEmployeeDto employee)
		{
			// Employee can have either a spouse OR domestic partner, not both or multiple
			if (CheckEmployeeValid(employee))
			{
				return _employeeRepository.AddAsync(employee);
			}
			return Task.FromResult(false); // invalid, can't have both spouse/partner
		}

		public Task<bool> DeleteAsyncEmployee(int id)
		{
			return _employeeRepository.DeleteAsync(id);
		}

		public Task<GetEmployeeDto> GetAsyncEmployeeById(int id)
		{
			return _employeeRepository.GetAsync(id);
		}

		public Task<GetEmployeeDto> GetAsyncEmployeeDtoByName(string name)
		{
			return _employeeRepository.GetAsync(name);
		}

		public Task<List<GetEmployeeDto>> GetAsyncEmployees()
		{
			return _employeeRepository.GetAllAsync();
		}

		/// <summary>
		/// Checks if employee meets business rules for having a single spouse OR domestic partner
		/// </summary>
		/// <returns></returns>
		public bool CheckEmployeeValid(GetEmployeeDto employee)
		{
			// Get the dependents
			bool hasSpousePartner = false;// dont know yet, so start with false
			var dependents = employee.Dependents;
			foreach (var dependent in dependents)
			{
				// if doesn't have spouse or domestic partner set, check for this dependent and if either, set it to true
				if (!hasSpousePartner && (dependent.Relationship == Relationship.Spouse || dependent.Relationship == Relationship.DomesticPartner))
				{
					hasSpousePartner = true;
				}
				// check if hasSpousePartner, is dependent also a spouse/partner
				if(hasSpousePartner && (dependent.Relationship == Relationship.Spouse || dependent.Relationship == Relationship.DomesticPartner))
				{
					return false;// invalid, can't have both
				}
			}
			//we made it, valid
			return true;
		}
	}
}
