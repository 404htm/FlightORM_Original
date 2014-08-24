using System;
using System.Collections.Generic;
namespace FlightORM.Common
{
	public interface ISPLoader
	{
		IList<FlightORM.Common.SPInfo> GetProcedures();
		IList<SPResult> GetOutputSchema(SPInfo procedure, IEnumerable<IParameterTestInfo> parameterInfo, out string ErrorMsg, bool useRollback = true);
		IList<SPParameter> GetParameters(SPInfo procedure);
		void TestExecution(SPInfo procedure, IEnumerable<IParameterTestInfo> parameterInfo, out string ErrorMsg, bool useRollback = true);
	}
}
