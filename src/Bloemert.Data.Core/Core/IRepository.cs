using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bloemert.Data.Core
{

	public interface IRepository<E>
		where E : BaseEntity
	{
		string EntityTypeName { get; set; }
		bool UseEffectiveVersioning { get; }


		E NewEntity();
		Task<E> NewEntityAsync();


		E GetEntity(Guid id);
		Task<E> GetEntityAsync(Guid id);


		E SaveEntity(E entity);
		Task<E> SaveEntityAsync(E entity);


		bool DeleteEntity(Guid id);
		Task<bool> DeleteEntityAsync(Guid id);

		bool DeleteEntity(E entity);
		Task<bool> DeleteEntityAsync(E entity);


		IList<E> ListQuery(Expression<Func<E, bool>> predicate = null);

		Task<IList<E>> ListQueryAsync(Expression<Func<E, bool>> predicate = null);
	}
}
