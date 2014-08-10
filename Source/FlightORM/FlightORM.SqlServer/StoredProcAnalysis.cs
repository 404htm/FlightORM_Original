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

		public StoredProcAnalysis(string connectionString)
		{
			 _connectionString = connectionString;
		}

		public IEnumerable<StoredProcedure> GetProcedures()
		{
			using (var cnn = new SqlConnection(_connectionString))
			{
				cnn.Open();
				var cmd = new SqlCommand("select routine_schema, specific_name, specific_catalog, created, last_altered from information_schema.routines where routine_type = 'PROCEDURE'", cnn);
				cmd.CommandType = CommandType.Text;
				var reader = cmd.ExecuteReader();

				var schema_index = reader.GetOrdinal("routine_schema");
				var name_index = reader.GetOrdinal("specific_name");
				var catalog_index = reader.GetOrdinal("specific_catalog");
				var created_index = reader.GetOrdinal("created");
				var modified_index = reader.GetOrdinal("created");

				foreach (var r in reader)
				{
					var sp = new StoredProcedure
					{
						Name = reader.GetString(name_index),
						Schema = reader.GetString(schema_index),
						DateCreated = reader.GetDateTime(created_index),
						DateModified = reader.GetDateTime(modified_index),
						Catalog = reader.GetString(catalog_index)
					};
					yield return sp;
				}
			}
		}

		//public IEnumerable<StoredProcedureDetail> GetProcedure
	}
}
