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
		public IEnumerable<StoredProcedure> GetProcedures()
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
					var sp = new StoredProcedure
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


		//public void LoadParameters(StoredProcedure procedure)
		//{
		

		//}

		//public void LoadParameters(List<StoredProcedure> procedures)
		//{
		

		//}

		//private IEnumerable<SPParameter> getParameters(SqlDataReader reader)
		//{
		//	var cols = reader.GetColumnLookup();
		//	foreach (var r in reader)
		//	{
		//		var param = new SPParameter{
		//			Name = 

		//		}
		//	}

		//}
	}
}
