using System;
using System.Collections.Generic;
namespace FlightORM.Common
{
	public interface ISPLoader
	{
		IList<FlightORM.Common.SPInfo> GetProcedures();
		void GetOutputSchema(SPInfo procedure, IList<IParameterTestInfo> parameterInfo, bool useRollback = true);
		IList<SPParameter> GetParameters(int procID);
		void ValidateProcedure(FlightORM.Common.SPInfo procedure, System.Data.SqlClient.SqlCommand SampleCommand, bool useRollback = true);
	}
}
