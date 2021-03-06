﻿using Bloemert.Lib.Common;
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

		private IList<string> EffectiveColumns = new List<string>
		{
			//"EffectiveStartedOn",
			//"EffectiveStartedBy",

			//"EffectiveModifiedOn",
			//"EffectiveModifiedBy",

			//"EffectiveEndedOn",
			//"EffectiveEndedBy",

			"Comment"
		};


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
				$"WHERE EffectiveEndedOn > GetDate() AND ID = @Id";
		}

		public virtual string CreateListQuery()
		{
			return
				$"SELECT {String.Join(", ", Repository.GetColumnsFromMetaData())} " + 
				$"FROM {Repository.TableName} " +
				$"WHERE EffectiveEndedOn > GetDate()";
		}

		public virtual string CreatePagedListQuery(string searchFilter, IList<string> sortColumns)
		{
			return
				// *** Max row count: View count query without search filters ***
				$"SELECT COUNT(*) " +
				$"FROM {Repository.TableName} " +
				$"WHERE EffectiveEndedOn > GetDate() " +
				$"; " +

				// *** Search row count: View count with search filters ***
				$"SELECT COUNT(*) " +
				$"FROM {Repository.TableName} " +
				$"WHERE EffectiveEndedOn > GetDate() " +
				$"; " +

				// *** Result data: Data query by search filters and pageOffset and pageRows ***
				$"SELECT {String.Join(", ", Repository.GetColumnsFromMetaData())} " +
				$"FROM {Repository.TableName} " +
				$"WHERE EffectiveEndedOn > GetDate() " +
				$"{searchFilter} " +
				$"ORDER BY {String.Join(", ", sortColumns)} " +
				$"OFFSET 10 ROWS FETCH NEXT 5 ROWS ONLY " +
				$"; ";
		}





		public virtual string CreateInsertQuery(IList<string> excludedColumns)
		{
			if (excludedColumns == null)
			{
				excludedColumns = EffectiveColumns;
			}
			else
			{
				excludedColumns = excludedColumns.Concat(EffectiveColumns).AsList();
			}

			StringBuilder valuesPart = new StringBuilder("VALUES ( ");
			
				//"@EffectiveStartedOn, " +
				//"@EffectiveStartedBy, " +
				//"@EffectiveModifiedOn, " +
				//"@EffectiveModifiedBy, " +
				//"@EffectiveEndedOn, " +
				//"@EffectiveEndedBy");

			var columns = Repository.GetColumnsFromMetaData(RequestedColumns.NO_PRIMARYKEY, excludedColumns);
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
				//$"( EffectiveStartedOn, EffectiveStartedBy, EffectiveModifiedOn, EffectiveModifiedBy, EffectiveEndedOn, EffectiveEndedBy, " +
				$"( { String.Join(", ", columns)} ) " +
				$"{valuesPart.ToString()};" +
				$"SELECT {String.Join(", ", Repository.GetColumnsFromMetaData(excludedColumns: excludedColumns))} " +
				$"FROM {Repository.TableName} " +
				$"WHERE EffectiveEndedOn > GetDate() " +
				$"AND ID = @@IDENTITY;"
				;
		}

		public virtual string CreateUpdateQuery(IList<string> excludedColumns = null)
		{
			if (excludedColumns == null)
			{
				excludedColumns = EffectiveColumns;
			}
			else
			{
				excludedColumns = excludedColumns.Concat(EffectiveColumns).AsList();
			}

			StringBuilder setPart = new StringBuilder("SET \n");

			foreach (string col in Repository.GetColumnsFromMetaData(RequestedColumns.NO_PRIMARYKEY, excludedColumns))
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
				$"WHERE EffectiveEndedOn > GetDate() AND ID = @Id";
		}


		public virtual string CreateDeleteQuery()
		{
			return
				$"UPDATE {Repository.TableName} " +
				$"SET EffectiveEndedOn = EffectiveModifiedOn = GetDate()" +
				$"WHERE EffectiveEndedOn > GetDate() AND ID = @Id";
		}

	}

}
