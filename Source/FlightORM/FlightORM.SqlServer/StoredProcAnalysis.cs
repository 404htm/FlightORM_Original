using FlightORM.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.SqlServer
{
	public class StoredProcAnalysis
	{
		string _connectionString;
		//bool _rollbackTestCalls;

		public StoredProcAnalysis(string connectionString)
		{
			 _connectionString = connectionString;

		}

		/// <summary>
		///	Lists all stored procedures available for the given database
		///	IMPORTANT: this does not load ancillary information such as parameters and return structure
		/// </summary>
		/// <returns>All stored procedures for given connection</returns>
		public IEnumerable<SPInfo> GetProcedures()
		{
			using (var cnn = new SqlConnection(_connectionString))
			{
				cnn.Open();
				var cmd = new SqlCommand(@"
				select sp.object_id as ID, sc.name as 'Schema', sp.name as Name, sp.create_date as Created, sp.modify_date as Modified from sys.objects sp 
				left join sys.schemas sc on sc.schema_id = sp.schema_id
				where sp.type = 'P'", cnn);
				cmd.CommandType = CommandType.Text;
				var reader = cmd.ExecuteReader();
				var index = reader.GetColumnLookup();

				foreach (var r in reader)
				{
					var sp = new SPInfo
					{
						Id = reader.GetInt32(index["ID"]),
						Name = reader.GetString(index["Name"]),
						Schema = reader.GetString(index["Schema"]),
						DateCreated = reader.GetDateTime(index["Created"]),
						DateModified = reader.GetDateTime(index["Modified"])
					};
					yield return sp;
				}
			}
		}


		public void LoadParameters(SPInfo procedure)
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
				var reader = cmd.ExecuteReader();
				procedure.InputParameters = getParameters(reader).Select(p => p.Item2).ToList();
			}
		}



		private IEnumerable<Tuple<int, SPParameter>> getParameters(SqlDataReader reader)
		{
			var index = reader.GetColumnLookup();
			foreach (var r in reader)
			{
				var procId = reader.GetInt32(index["ProcedureId"]);

				var typeInfo = new DbTypeInfo();
				typeInfo.TypeName = reader.GetString(index["t.name"]);
				typeInfo.MaxLength = reader.GetInt16(index["t.maxLen"]);
				typeInfo.Precision = reader.GetByte(index["t.precision"]);
				typeInfo.Scale = reader.GetByte(index["t.scale"]);

				var param = new SPParameter();
				param.Name = reader.GetString(index["Name"]);
				param.Position = reader.GetInt32(index["position"]);
				param.TypeInfo = typeInfo;
				param.IsOutput = reader.GetBoolean(index["output"]);
				param.IsReadOnly = reader.GetBoolean(index["readonly"]);
				param.HasDefault = reader.GetBoolean(index["hasDefault"]);
				param.DefaultValue = reader.GetValue(index["defaultValue"]);

				yield return Tuple.Create(procId, param);
			}
		}

		public void LoadOutputSchema(SPInfo procedure, SqlCommand SampleCommand, bool useRollback = true)
		{
			//todo: remove transactions if useRollback is false
			using(var con = new SqlConnection(_connectionString))
			{
				SqlTransaction testTransaction = null;
				SqlDataReader reader;

				con.Open();
				if (useRollback) testTransaction = con.BeginTransaction("SpTestTransaction");
				SampleCommand.Connection = con;
				SampleCommand.Transaction = testTransaction;

				try
				{
					reader = SampleCommand.ExecuteReader();
					procedure.IsValid = true;
					procedure.Error = null;
				}
				catch(SqlException ex)
				{
					procedure.IsValid = false;
					procedure.Error = ex.Message;
					return;
				}
				
				procedure.OutputData = new List<SPResult>();


				var resultIndex = 0;
				do
				{
					var result = new SPResult() { ResultIndex = resultIndex };

					for (int c = 0; c < reader.FieldCount;c++)
					{
						result.Columns.Add(new ResultElement{Name = reader.GetName(c), Type = reader.GetFieldType(c)});
					}

					procedure.OutputData.Add(result);
				}
				while(reader.NextResult());

				reader.Close();
				if (useRollback) testTransaction.Rollback();
			}
		}

		public void ValidateProcedure(SPInfo procedure, SqlCommand SampleCommand, bool useRollback = true)
		{
			//todo: remove transactions if useRollback is false
			using (var con = new SqlConnection(_connectionString))
			{
				SqlTransaction testTransaction = null;
				SqlDataReader reader;

				con.Open();
				if (useRollback) testTransaction = con.BeginTransaction("SpTestTransaction");
				SampleCommand.Connection = con;
				SampleCommand.Transaction = testTransaction;

				try
				{
					reader = SampleCommand.ExecuteReader();
					procedure.IsValid = true;
					procedure.Error = null;
				}
				catch (SqlException ex)
				{
					procedure.IsValid = false;
					procedure.Error = ex.Message;
					return;
				}

				reader.Close();
				if (useRollback) testTransaction.Rollback();
			}
		}
	}
}
