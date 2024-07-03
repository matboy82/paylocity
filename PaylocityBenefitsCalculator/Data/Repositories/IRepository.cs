using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public interface IRepository<T> where T : class
	{
		Task<T> GetAsync(int id);
		Task<T> GetAsync(string name);
		Task<List<T>> GetAllAsync();
		Task<bool> AddAsync(T entity);
		Task<bool> UpdateAsync(T entity);
		Task<bool> DeleteAsync(int id);
	}
}
