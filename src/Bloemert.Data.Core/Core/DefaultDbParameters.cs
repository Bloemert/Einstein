using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Bloemert.Data.Core
{
	public class DefaultDbParameters : IDbParameters
	{

		public DynamicParameters DynamicParameters { get; set; } = new DynamicParameters();

		public IEnumerable<string> ParameterNames
		{
			get
			{
				return DynamicParameters.ParameterNames;
			}
		}



		public DefaultDbParameters()
		{
		}

		public DefaultDbParameters(object dbParams)
		{
			AddDynamicParams(dbParams);
		}

		public DefaultDbParameters(string name, object value)
		{
			AddInputParameter(name, value);
		}



		public void Add(string name)
		{
			DynamicParameters.Add(name, null, default(DbType?), default(ParameterDirection?), default(int?));
		}
		public void Add(string name, object value)
		{
			DynamicParameters.Add(name, value, default(DbType?), default(ParameterDirection?), default(int?));
		}
		public void Add(string name, object value, DbType? dbType)
		{
			DynamicParameters.Add(name, value, dbType, default(ParameterDirection?), default(int?));
		}
		public void Add(string name, object value, DbType? dbType, ParameterDirection? direction)
		{
			DynamicParameters.Add(name, value, dbType, direction);
		}
		public void Add(string name, object value, DbType? dbType, ParameterDirection? direction, int? size)
		{
			DynamicParameters.Add(name, value, dbType, direction, size);
		}


		public void AddDynamicParams(dynamic param)
		{
			DynamicParameters.AddDynamicParams(param);
		}


		public void AddInputParameter(string name, object value)
		{
			DynamicParameters.Add(name, value, default(DbType?), ParameterDirection.Input, default(int?));
		}

		public void AddInputParameter(string name, object value, DbType? dbType, int? size)
		{
			DynamicParameters.Add(name, value, dbType, ParameterDirection.Input, size);
		}


		public void AddInputParameter<T>(string name, T value)
		{
			DynamicParameters.Add(name, value, default(DbType?), ParameterDirection.Input, default(int?));
		}
		public void AddInputParameter<T>(string name, T value, DbType? dbType, int? size)
		{
			DynamicParameters.Add(name, value, dbType, ParameterDirection.Input, size);
		}


		public void AddOutputParameter(string name)
		{
			DynamicParameters.Add(name, default(DbType?), null, ParameterDirection.Output, default(int?));
		}
		public void AddOutputParameter(string name, DbType? dbType, int? size)
		{
			DynamicParameters.Add(name, dbType, null, ParameterDirection.Output, size);
		}


		public void AddReturnValue(string name)
		{
			DynamicParameters.Add(name, default(DbType?), null, ParameterDirection.ReturnValue, default(int?));
		}
		public void AddReturnValue(string name, DbType? dbType, int? size)
		{
			DynamicParameters.Add(name, dbType, null, ParameterDirection.ReturnValue, size);
		}


		public void CreateParamsWithTemplate(object template)
		{
			DynamicParameters = new DynamicParameters(template);
		}

		public T Get<T>(string name)
		{
			return DynamicParameters.Get<T>(name);
		}
	}
}
