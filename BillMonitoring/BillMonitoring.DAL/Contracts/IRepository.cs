using System;
using System.Collections.Generic;
using System.Text;

namespace BillMonitoring.DAL.Contracts
{
	public interface IRepository<T>
		where T : class
	{
		IEnumerable<T> GetAll();

		T GetOne(string id);

		void Create(T item);

		void Update(T item);

		void Delete(string id);
	}
}
