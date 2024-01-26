using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DAL.Interface
{
	public interface IRepository<T> where T : class
	{
		Task<bool> CreateAsync(T entity);

		Task<IEnumerable<T>> GetAllAsync();

		Task<T> GetByIdAsync(int id);

		Task<bool> DeleteAsync(T entity);
	}
}
