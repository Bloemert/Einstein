using System.Collections.Generic;

namespace Bloemert.Data.Core.Templates
{
	public interface IQueryTemplate
	{
		IEnumerable<DbColumnInfo> EntityMetadata { get; set; }


		string CreateMetaDataQuery();

		string CreateSelectQuery();

		string CreateListQuery();

		string CreatePagedListQuery(string searchFilter, IList<string> sortColumns);

		string CreateInsertQuery(IList<string> excludedColumns = null);

		string CreateUpdateQuery(IList<string> excludedColumns = null);

		string CreateDeleteQuery();

	}
}