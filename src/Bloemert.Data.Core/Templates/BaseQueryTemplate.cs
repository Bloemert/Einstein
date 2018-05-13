using Bloemert.Lib.Common;
using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bloemert.Data.Core.Templates
{
	public class BaseQueryTemplate<E> : IQueryTemplate
		where E : IEntity
	{

		protected IRepository<E> Repository { get; set; }

		// Entity Metadata
		public IEnumerable<DbColumnInfo> EntityMetadata { get; set; } = null;



		public BaseQueryTemplate(IRepository<E> repository)
		{
			Repository = repository;
		}


		public virtual string CreateMetaDataQuery()
		{
			return
				$"select " +
				$"	scol.name ColumnName " +
				$", scol.is_identity IsIdentity " +
				$", scol.is_nullable IsNullable " +
				$", scol.is_rowguidcol IsRowGuidCol " +
				$", scol.is_xml_document IsXmlDocument " +
				$", scol.is_computed IsComputed " +
				$", stype.name DataType " +
				$", scol.max_length MaxLength " +
				$", scol.precision Precision " +
				$", scol.scale Scale " +
				$", object_definition(scol.default_object_id) DefaultValue " +
				$"from sys.columns scol " +
				$"join sys.types stype " +
				$"	on stype.user_type_id = scol.user_type_id " +
				$"where scol.object_id = OBJECT_ID('{Repository.TableName}')";
		}

		public virtual string CreateSelectQuery()
		{
			return
				$"SELECT " +
				String.Join(", ", Repository.GetColumnsFromMetaData()) + " " +
				$"FROM {Repository.TableName} " +
				$"WHERE DELETED = 0 AND ID = @Id";
		}

		public virtual string CreateListQuery()
		{
			return
				$"SELECT {String.Join(", ", Repository.GetColumnsFromMetaData())} " + 
				$"FROM {Repository.TableName} " +
				$"WHERE DELETED = 0";
		}

		public virtual string CreatePagedListQuery(string searchFilter, IList<string> sortColumns)
		{
			return
				// *** Max row count: View count query without search filters ***
				$"SELECT COUNT(*) " +
				$"FROM {Repository.TableName} " +
				$"WHERE DELETED = 0 " +
				$"; " +

				// *** Search row count: View count with search filters ***
				$"SELECT COUNT(*) " +
				$"FROM {Repository.TableName} " +
				$"WHERE DELETED = 0 " +
				$"; " +

				// *** Result data: Data query by search filters and pageOffset and pageRows ***
				$"SELECT {String.Join(", ", Repository.GetColumnsFromMetaData())} " +
				$"FROM {Repository.TableName} " +
				$"WHERE DELETED = 0 " +
				$"{searchFilter} " +
				$"ORDER BY {String.Join(", ", sortColumns)} " +
				$"OFFSET 10 ROWS FETCH NEXT 5 ROWS ONLY " +
				$"; ";
		}





		public virtual string CreateInsertQuery()
		{
			StringBuilder valuesPart = new StringBuilder("VALUES ( ");

			var columns = Repository.GetColumnsFromMetaData(RequestedColumns.NO_PRIMARYKEY);
			foreach (string col in columns)
			{
				if (valuesPart.Length > 10)
				{
					valuesPart.AppendLine(",");
				}

				valuesPart.AppendLine($" @{col}");
			}
			valuesPart.AppendLine(" ) ");

			return
				$"INSERT INTO {Repository.TableName} " +
				$"( {String.Join(", ", columns)} ) " +
				$"{valuesPart.ToString()};" +
				$"SELECT {String.Join(", ", Repository.GetColumnsFromMetaData())} " +
				$"FROM {Repository.TableName} " +
				$"WHERE DELETED = 0 " +
				$"AND ID = @@IDENTITY;"
				;
		}

		public virtual string CreateUpdateQuery()
		{
			StringBuilder setPart = new StringBuilder("SET \n");

			foreach (string col in Repository.GetColumnsFromMetaData(RequestedColumns.NO_PRIMARYKEY))
			{
				if (setPart.Length > 5)
				{
					setPart.AppendLine(",");
				}

				setPart.AppendLine($"	{col} = @{col}");
			}

			return
				$"UPDATE {Repository.TableName} " +
				$"{setPart.ToString()} " +
				$"WHERE DELETED = 0 AND ID = @Id";
		}


		public virtual string CreateDeleteQuery()
		{
			return
				$"UPDATE {Repository.TableName} " +
				$"SET DELETED = 1 " +
				$"WHERE DELETED = 0 AND ID = @Id";
		}

	}

}
