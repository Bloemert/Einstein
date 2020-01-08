using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Einstein.DataAccessLayer.Core
{

  public interface IRepository<E>
		where E : BaseEntity
  {
	 string EntityTypeName { get; set; }
	 bool UseEffectiveVersioning { get; }


	 Task<E> NewEntityAsync();

	 Task<E> GetEntityAsync(Guid id);

	 Task<E> SaveEntityAsync(E entity);

	 Task<bool> DeleteEntityAsync(Guid id);
	 Task<bool> DeleteEntityAsync(E entity);


	 Task<E> FirstOrDefaultAsync(Expression<Func<E, bool>> predicate = null);

	 Task<IList<E>> ListQueryAsync(Expression<Func<E, bool>> predicate = null);
  }
}
