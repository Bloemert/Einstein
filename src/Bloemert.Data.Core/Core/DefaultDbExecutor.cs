using System.Collections.Generic;
using System.Linq;
using Dapper;
using System;
using Serilog;
using System.Data;
using static Dapper.SqlMapper;
using System.Threading.Tasks;

namespace Bloemert.Data.Core
{
	public class DefaultDbExecutor : IDbExecutor
	{
		private ILogger Log { get; }

		public IDbConnectionFactory ConnectionFactory { get; set; }


		public DefaultDbExecutor(ILogger log, IDbConnectionFactory connFactory)
		{
			Log = log;
			ConnectionFactory = connFactory;
		}



		public E Select<E>(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("Select:\n{query}\n on entity: {FullName}\n using parameters: {DynamicParameters}!", query, typeof(E).FullName, dynDbParams);

			E result = default(E);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					result = conn.Query<E>(query, dynDbParams).FirstOrDefault();
				}
			}
			catch ( Exception ex )
			{
				Log.Error(ex, "Select:\n{query}\n on entity: {FullName}\n failed using: {DynamicParameters}!", query, typeof(E).FullName, dynDbParams);
				throw (ex);
			}

			return result;
		}

		public async Task<E> SelectAsync<E>(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("SelectAsync:\n{query}\n on entity: {FullName}\n using parameters: {DynamicParameters}!", query, typeof(E).FullName, dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					var result = await conn.QueryAsync<E>(query, dynDbParams);
					return result.FirstOrDefault();
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "SelectAsync: {query}\n on entity: {FullName}\n failed using: {DynamicParameters}!", query, typeof(E).FullName, dynDbParams);
				throw (ex);
			}
		}



		public dynamic Select(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("Select: {query}\n on dynamic entity with parameters: {DynamicParameters}!", query, dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					return conn.Query(query, dynDbParams).FirstOrDefault();
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Select: {query}\n on dynamic entity failed with: {DynamicParameters}!", query, dynDbParams);
				throw (ex);
			}
		}

		public async Task<dynamic> SelectAsync(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("SelectAsync: {query}\n on dynamic entity with parameters: {DynamicParameters}!", query, dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					var result = await conn.QueryAsync(query, dynDbParams);
					return result.FirstOrDefault();
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "SelectAsync: {query}\n on dynamic entity failed with: {DynamicParameters}!", query, dynDbParams);
				throw (ex);
			}
		}



		public IList<E> List<E>(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("List: {query}\n for entity: {FullName}\n using parameters : {DynamicParameters}!", query, typeof(E).FullName, dynDbParams);

			IList<E> result = default(IList<E>);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					result = conn.Query<E>(query, dynDbParams).ToList();
				}
			}
			catch ( Exception ex )
			{
				Log.Error(ex, "List: {query}\n for entity: {FullName}\n failed using: {DynamicParameters}!", query, typeof(E).FullName, dynDbParams);
				throw (ex);
			}

			return result;
		}

		public async Task<IList<E>> ListAsync<E>(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("ListAsync: {query}\n for entity: {FullName}\n using parameters: {DynamicParameters}!", query, typeof(E).FullName, dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					var result = await conn.QueryAsync<E>(query, dynDbParams);
					return result.ToList();
				}
			}
			catch ( Exception ex )
			{
				Log.Error(ex, "ListAsync: {query}\n for entity: {FullName}\n failed using: {DynamicParameters}!", 
									query, 
									typeof(E).FullName, 
									dynDbParams);
				throw (ex);
			}
		}



		public IEnumerable<dynamic> List(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("List: {query}\n for dynamic entity using parameters: {DynamicParameters}!", query, dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					return conn.Query<dynamic>(query, dynDbParams);
				}
			} 
			catch ( Exception ex )
			{
				Log.Error(ex, "List: {query}\n for dynamic entity failed using: {DynamicParameters}!", query, dynDbParams);
				throw (ex);
			}
		}
		
		public async Task<IEnumerable<dynamic>> ListAsync(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("ListAsync: {query}\n for dynamic entity using parameters: {DynamicParameters}!", query, dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					return await conn.QueryAsync<dynamic>(query, dynDbParams);
				}
			}
			catch ( Exception ex )
			{
				Log.Error(ex, "ListAsync: {query}\n for dynamic entity failed using: {DynamicParameters}!", query, dynDbParams);
				throw (ex);
			}
		}



		public IDictionary<KT, VT> Dictionary<KT, VT>(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("Dictionary: {query}\n for KeyType = {typeof(KT)FullName}, ValueType = {typeof(VT)FullName}\n entity using parameters: {DynamicParameters}!",
								query,
								typeof(KT).FullName,
								typeof(VT).FullName,
								dynDbParams);
			IDictionary<KT, VT> result = default(IDictionary<KT, VT>);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					result = conn.Query(query, dynDbParams).ToDictionary(row => (KT)row.Key, row => (VT)row.Value);
				}
			}
			catch (Exception ex )
			{
				Log.Error(ex, "Dictionary: {query}\n for KeyType = {typeof(KT)FullName}, ValueType = {typeof(VT)FullName}\n entity failed using: {DynamicParameters}!",
									query,
									typeof(KT).FullName, 
									typeof(VT).FullName, 
									dynDbParams);
				throw (ex);
			}

			return result;
		}

		public async Task<IDictionary<KT, VT>> DictionaryAsync<KT, VT>(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("Dictionary: {query}\n for KeyType = {typeof(KT)FullName}, ValueType = {typeof(VT)FullName}\n entity using parameters: {DynamicParameters}!",
					query,
					typeof(KT).FullName,
					typeof(VT).FullName,
					dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					var result = await conn.QueryAsync(query, dynDbParams);
					return result.ToDictionary(row => (KT)row.Key, row => (VT)row.Value);
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "DictionaryAsync: {query}\n for KeyType = {typeof(KT)FullName}, ValueType = {typeof(VT)FullName}\n entity failed using: {DynamicParameters}!",
									query,
									typeof(KT).FullName, 
									typeof(VT).FullName, 
									dynDbParams);
				throw (ex);
			}
		}



		public FT ExecuteScalar<FT>(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("ExecuteScalar: {query}\n for Type = {typeof(FT)FullName}\n using parameters: {DynamicParameters}!",
								query,
								typeof(FT).FullName,
								dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					return conn.ExecuteScalar<FT>(query, dynDbParams);
				}
			}
			catch ( Exception ex )
			{
				Log.Error(ex, "ExecuteScalar: {query}\n for Type = {typeof(FT)FullName}\n failed using: {DynamicParameters}!",
									query,
									typeof(FT).FullName, 
									dynDbParams);
				throw (ex);
			}
		}

		public async Task<FT> ExecuteScalarAsync<FT>(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("ExecuteScalarAsync: {query}\n for Type = {typeof(FT)FullName}\n using parameters: {DynamicParameters}!",
					query,
					typeof(FT).FullName,
					dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					return await conn.ExecuteScalarAsync<FT>(query, dynDbParams);
				}
			}
			catch (Exception ex )
			{
				Log.Error(ex, "ExecuteScalarAsync: {query}\n for Type = {typeof(FT)FullName}\n failed using: {DynamicParameters}!",
									query,
									typeof(FT).FullName, 
									dynDbParams);
				throw (ex);
			}
		}



		public void Execute(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("Execute: {query}\n using parameters: {DynamicParameters}!", query, dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					conn.Execute(query, dynDbParams);
				}
			}
			catch ( Exception ex )
			{
				Log.Error(ex, "Execute: {query}\n failed using: {DynamicParameters}!", query, dynDbParams);
				throw (ex);
			}
		}

		public async Task ExecuteAsync(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("ExecuteAsync: {query}\n using parameters: {DynamicParameters}!", query, dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					await conn.ExecuteAsync(query, dynDbParams);
				}
			}
			catch ( Exception ex )
			{
				Log.Error(ex, "ExecuteAsync: {query}\n failed using: {DynamicParameters}!", query, dynDbParams);
				throw (ex);
			}
		}



		public E ExecuteAndQuery<E>(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("ExecuteAndQuery: {query}\n using parameters: {dynDbParams}!", query, dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					return conn.Query<E>(query, dynDbParams).FirstOrDefault();
				}
			}
			catch (Exception ex )
			{
				Log.Error(ex, "ExecuteAndQuery: {query}\n failed using: {dynDbParams}!", query, dynDbParams);
				throw (ex);
			}
		}


		public async Task<E> ExecuteAndQueryAsync<E>(string query, IDbParameters dbParams)
		{
			DynamicParameters dynDbParams = dbParams != null ? dbParams.DynamicParameters : null;

			Log.Debug("ExecuteAndQueryAsync: {query}\n using parameters: {dynDbParams}!", query, dynDbParams);

			try
			{
				using (var conn = ConnectionFactory.Create())
				{
					var qryResult = await conn.QueryAsync<E>(query, dynDbParams);
					return qryResult.FirstOrDefault();
				}
			}
			catch ( Exception ex )
			{
				Log.Error(ex, "ExecuteAndQueryAsync: {query}\n failed using: {dynDbParams}!", query, dynDbParams);
				throw (ex);
			}
		}

	}
}
