using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dtos.Dependent;
using Data.Models;

namespace Data.Repositories
{
	/// <summary>
	/// Adding just in case, this would indicate a table in the db, and we'd need some FK relationships
	/// </summary>
	public class DependentRepository : IRepository<GetDependentDto>
	{

		private List<GetDependentDto> _dependents = new List<GetDependentDto>();
		public DependentRepository() 
		{
			InitDependentList();
		}

		// Just fill the list since we don't have a db
		private void InitDependentList()
		{
			_dependents.Add(new()
			{
				Id = 1,
				FirstName = "Spouse",
				LastName = "Morant",
				Relationship = Relationship.Spouse,
				DateOfBirth = new DateTime(1998, 3, 3)
			});

			_dependents.Add(new()
			{
				Id = 2,
				FirstName = "Child1",
				LastName = "Morant",
				Relationship = Relationship.Child,
				DateOfBirth = new DateTime(2020, 6, 23)
			});
			_dependents.Add(new()
			{
				Id = 3,
				FirstName = "Child2",
				LastName = "Morant",
				Relationship = Relationship.Child,
				DateOfBirth = new DateTime(2021, 5, 18)
			});
			_dependents.Add(new()
			{
				Id = 4,
				FirstName = "DP",
				LastName = "Jordan",
				Relationship = Relationship.DomesticPartner,
				DateOfBirth = new DateTime(1974, 1, 2)
			});
		}

		public Task<bool> AddAsync(GetDependentDto entity)
		{
			_dependents.Add(entity);
			return Task.FromResult(_dependents.Contains(entity));
		}

		public Task<bool> DeleteAsync(int id)
		{
			if(_dependents.Contains(_dependents.FirstOrDefault(x => x.Id == id)))
			{
				_dependents.Remove(_dependents.FirstOrDefault(x => x.Id == id));
			}
			
			return Task.FromResult(_dependents.Contains(_dependents.FirstOrDefault(x => x.Id == id)));
		}

		public Task<List<GetDependentDto>> GetAllAsync()
		{
			return Task.FromResult(_dependents);
		}

		public Task<GetDependentDto> GetAsync(int id)
		{
			return Task.FromResult(_dependents.FirstOrDefault(x => x.Id == id));
		}

		public Task<GetDependentDto> GetAsync(string name)
		{
			return Task.FromResult(_dependents.FirstOrDefault(x => x.FirstName == name));
		}

		public Task<bool> UpdateAsync(GetDependentDto entity)
		{
			if(_dependents.Contains(entity))
			{
				_dependents.Remove(entity);
				_dependents.Add(entity);
				return Task.FromResult(_dependents.Contains(entity));
			}
			return Task.FromResult(false);
		}
	}
}
