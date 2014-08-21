using System;
using System.Collections.Generic;
namespace FlightORM.Common
{
	public interface IStoredProcAnalyzer
	{
		System.Collections.Generic.IEnumerable<FlightORM.Common.SPInfo> GetProcedures();
		void LoadOutputSchema(FlightORM.Common.SPInfo procedure, IDictionary<string, string> values, bool useRollback = true);
		void LoadParameters(FlightORM.Common.SPInfo procedure);
		void ValidateProcedure(FlightORM.Common.SPInfo procedure, System.Data.SqlClient.SqlCommand SampleCommand, bool useRollback = true);
	}
}
