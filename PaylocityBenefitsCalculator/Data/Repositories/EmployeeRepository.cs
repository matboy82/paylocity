using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Data.Dtos.Dependent;
using Data.Dtos.Employee;
using Data.Models;

namespace Data.Repositories
{
	/// <summary>
	/// Repository for <see cref="Employee"/>
	/// Tracks the list of employees and any changes to it, acts as an in memory store. Normally this layer would utilize something like EF Core or Dapper to query the database.
	/// Uses DTOs to represent the data, which would normally be a conversion from the DB model (as in the models folder).
	/// </summary>
	public class EmployeeRepository : IRepository<GetEmployeeDto>
	{
		private List<GetEmployeeDto> _employees = new List<GetEmployeeDto>();
		
		public EmployeeRepository() { 
			InitEmployeeList();
		}
		/// <summary>
		/// Initializes the list of employees, simply adds to list since there's only 3, normally would be seeding the db
		/// </summary>
		private void InitEmployeeList()
		{
			_employees.Add(new()
			{
				Id = 1,
				FirstName = "LeBron",
				LastName = "James",
				Salary = 75420.99m,
				DateOfBirth = new DateTime(1984, 12, 30)
			});
			_employees.Add(new()
			{
				Id = 2,
				FirstName = "Ja",
				LastName = "Morant",
				Salary = 92365.22m,
				DateOfBirth = new DateTime(1999, 8, 10),
				Dependents = new List<GetDependentDto>
				{
					new()
					{
						Id = 1,
						FirstName = "Spouse",
						LastName = "Morant",
						Relationship = Relationship.Spouse,
						DateOfBirth = new DateTime(1998, 3, 3)
					},
					new()
					{
						Id = 2,
						FirstName = "Child1",
						LastName = "Morant",
						Relationship = Relationship.Child,
						DateOfBirth = new DateTime(2020, 6, 23)
					},
					new()
					{
						Id = 3,
						FirstName = "Child2",
						LastName = "Morant",
						Relationship = Relationship.Child,
						DateOfBirth = new DateTime(2021, 5, 18)
					}
				}
			});
			_employees.Add(new()
			{
				Id = 3,
				FirstName = "Michael",
				LastName = "Jordan",
				Salary = 143211.12m,
				DateOfBirth = new DateTime(1963, 2, 17),
				Dependents = new List<GetDependentDto>
				{
					new()
					{
						Id = 4,
						FirstName = "DP",
						LastName = "Jordan",
						Relationship = Relationship.DomesticPartner,
						DateOfBirth = new DateTime(1974, 1, 2)
					}
				}
			});
		}

		/// <summary>
		/// Adds an employee
		/// </summary>
		/// <param name="entity"></param>
		/// <returns>Task boolean on success of insertion operation</returns>
		public Task<bool> AddAsync(GetEmployeeDto entity)
		{
			if (_employees.Contains(entity))
			{
				return Task.FromResult(false);
			}

			_employees.Add(entity);
			return Task.FromResult(_employees.Contains(entity));
		}

		/// <summary>
		/// Async deletes an employee
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Success of deletion</returns>
		public Task<bool> DeleteAsync(int id)
		{
			var employee = _employees.FirstOrDefault(x => x.Id == id);
			if (employee != null)
			{
				_employees.Remove(employee);
				return Task.FromResult(!_employees.Contains(employee));
			}
			return Task.FromResult(false);
		}

		/// <summary>
		/// Get all employees
		/// </summary>
		/// <returns>Task of all employees</returns>
		public Task<List<GetEmployeeDto>> GetAllAsync()
		{
			return Task.FromResult<List<GetEmployeeDto>>(_employees);
		}

		/// <summary>
		/// Get an employee by their id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Task of employee or null</returns>
		public Task<GetEmployeeDto> GetAsync(int id)
		{
			return Task.FromResult(_employees.FirstOrDefault(x => x.Id == id));
		}

		/// <summary>
		/// Get an employee by their name
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Task of employee or null</returns>
		public Task<GetEmployeeDto> GetAsync(string name)
		{
			return Task.FromResult(_employees.FirstOrDefault(x => x.FirstName == name || x.LastName == name));
		}

		public Task<bool> UpdateAsync(GetEmployeeDto entity)
		{
			if (_employees.Contains(entity))
			{
				_employees.Remove(entity);
				_employees.Add(entity);
				return Task.FromResult(_employees.Contains(entity));
			}
			return Task.FromResult(false);
		}
	}
}
