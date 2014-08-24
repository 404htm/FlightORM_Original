using FlightORM.Common;
using FlightORM.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.SqlServer
{
	public class SPLoader : ISPLoader
	{
		string _connectionString;

		public SPLoader(string connectionString)
		{
			 _connectionString = connectionString;
		}

		/// <summary>
		///	Lists all stored procedures available for the given database
		/// </summary>
		/// <returns>All stored procedures for given connection</returns>
		public IList<SPInfo> GetProcedures()
		{
			using (var cnn = new SqlConnection(_connectionString))
			{
				cnn.Open();
				var cmd = new SqlCommand(@"
				select sp.object_id as ID, sc.name as 'Schema', sp.name as Name, sp.create_date as Created, sp.modify_date as Modified from sys.objects sp 
				left join sys.schemas sc on sc.schema_id = sp.schema_id
				where sp.type = 'P'", cnn);
				cmd.CommandType = CommandType.Text;
				using(var reader = cmd.ExecuteReader())
				{
					return getProcedures(reader).ToList();
				}
			}
		}

		/// <summary>
		/// Gets a list of the parameters for the specified stored procedure
		/// </summary>
		/// <param name="procedure"></param>
		/// <returns>All parameters for the specified query</returns>
		public IList<SPParameter> GetParameters(SPInfo procedure)
		{
			var query = @"select p.name as Name, p.object_id as ProcedureId, p.parameter_id as 'position', t.name as 't.name', 
							p.is_output as 'output', p.max_length as 't.maxLen', p.precision as 't.precision', 
							p.scale as 't.scale', p.is_readonly as readonly, p.has_default_value as hasDefault, 
							default_value as defaultValue
						from sys.parameters p
						left join sys.types t ON t.user_type_id = p.user_type_id
						where p.object_id = @sp_id";
			
			using (var cnn = new SqlConnection(_connectionString))
			{
				cnn.Open();
				var cmd = new SqlCommand(query, cnn);
				cmd.Parameters.Add(new SqlParameter("@sp_id", procedure.Id));
				using(var reader = cmd.ExecuteReader())
				{
					return getParameters(reader).Select(λ => λ.Item2).ToList();
				}
			}
		}

		/// <summary>
		/// Runs the specified stored procedure and examines the output schema
		/// Calling code should handle SQLExceptions for cases where the command is invalid
		/// </summary>
		/// <param name="procedure">The stored procedure to be tested</param>
		/// <param name="parameterInfo">The required parameter, type info, and test value</param>
		/// <param name="useRollback">If true the query will be run inside a transaction and rolled back</param>
		/// <returns>A list of output schemas</returns>
		public IList<SPResult> GetOutputSchema(SPInfo procedure, IEnumerable<IParameterTestInfo> parameterInfo, out string ErrorMsg, bool useRollback = true)
		{
			using(var con = new SqlConnection(_connectionString))
			{
				var resultSet = new List<SPResult>();
				SqlTransaction testTransaction = null;
				SqlDataReader reader;

				con.Open();
				if (useRollback) testTransaction = con.BeginTransaction("SpTestTransaction");

				var cmd = new SqlCommand(string.Format("[{0}].{1}", procedure.Schema, procedure.Name));
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Connection = con;
				cmd.Transaction = testTransaction;

				//Populate test parameters
				foreach(var pi in parameterInfo)
				{
					var p = new SqlParameter(pi.Name, pi.DBType);
					p.Value = TypeHelpers.ConvertToType(pi.SampleValue, pi.DotNetType);
					cmd.Parameters.Add(p);
				}

				try {
					using( reader = cmd.ExecuteReader())
					{
						var resultIndex = 0;
						do
						{
							//Determine schema for each set returned by stored procedure
							var result = new SPResult(resultIndex);
							for (int c = 0; c < reader.FieldCount; c++)
							{
								result.Columns.Add(new ResultElement { Name = reader.GetName(c), Type = reader.GetFieldType(c) });
							}
							resultSet.Add(result);
						}
						while (reader.NextResult());
					}
					ErrorMsg = null;
				}
				catch(SqlException ex)
				{
					ErrorMsg = ex.Message;
				}
				finally
				{
					if (useRollback) testTransaction.Rollback();
				}

				return resultSet;
			}
		}

		/// <summary>
		/// Runs the specified stored procedure but doesn't read the result
		/// This is used to make sure the command/query is actually valid
		/// </summary>
		/// <param name="procedure">The stored procedure to be tested</param>
		/// <param name="parameterInfo">The required parameter, type info, and test value</param>
		/// <param name="useRollback">If true the query will be run inside a transaction and rolled back</param>
		public void TestExecution(SPInfo procedure, IEnumerable<IParameterTestInfo> parameterInfo, out string ErrorMsg, bool useRollback = true)
		{
			using (var con = new SqlConnection(_connectionString))
			{
				var resultSet = new List<SPResult>();
				SqlTransaction testTransaction = null;
				SqlDataReader reader = null;

				con.Open();
				if (useRollback) testTransaction = con.BeginTransaction("SpTestTransaction");

				var cmd = new SqlCommand(string.Format("[{0}].{1}", procedure.Schema, procedure.Name));
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Connection = con;
				cmd.Transaction = testTransaction;

				//Populate test parameters
				foreach (var pi in parameterInfo)
				{
					var p = new SqlParameter(pi.Name, pi.DBType);
					p.Value = TypeHelpers.ConvertToType(pi.SampleValue, pi.DotNetType);
					cmd.Parameters.Add(p);
				}

				try
				{
					reader = cmd.ExecuteReader();
					ErrorMsg = null;
				}
				catch(SqlException ex) 
				{ 
					ErrorMsg = ex.Message; 
				}
				finally 
				{ 
					if(reader != null) reader.Close();
				}; 
			}
		}

		#region Private Methods

		private IEnumerable<Tuple<int, SPParameter>> getParameters(SqlDataReader reader)
		{
			var index = reader.GetColumnLookup();
			foreach (var r in reader)
			{
				var procId = reader.GetInt32(index["ProcedureId"]);

				var typeName = reader.GetString(index["t.name"]);
				var maxLength = reader.GetInt16(index["t.maxLen"]);
				var precision = reader.GetByte(index["t.precision"]);
				var scale = reader.GetByte(index["t.scale"]);
				var typeInfo = new DbTypeInfo(typeName, null, maxLength, precision, scale);
				
				var name = reader.GetString(index["Name"]);
				var position = reader.GetInt32(index["position"]);
				var isOutput = reader.GetBoolean(index["output"]);
				var isReadOnly = reader.GetBoolean(index["readonly"]);
				var defaultValue = reader.GetValue(index["defaultValue"]);
				var param = new SPParameter(name, typeInfo, position, isOutput, isReadOnly, defaultValue);

				yield return Tuple.Create(procId, param);
			}
		}

		private IEnumerable<SPInfo> getProcedures(SqlDataReader reader)
		{
			var index = reader.GetColumnLookup();

			foreach (var r in reader)
			{
				var id = reader.GetInt32(index["ID"]);
				var name = reader.GetString(index["Name"]);
				var schema = reader.GetString(index["Schema"]);
				var dateCreated = reader.GetDateTime(index["Created"]);
				var dateModified = reader.GetDateTime(index["Modified"]);

				var sp = new SPInfo(id, name, schema, dateCreated, dateModified);
				yield return sp;
			}
		}
		#endregion
	}
}
