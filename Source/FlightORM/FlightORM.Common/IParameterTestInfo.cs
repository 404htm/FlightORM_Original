using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Common
{
	public interface IParameterTestInfo
	{
		string Name { get; }
		string SampleValue { get; }
		string DBType { get; }
		string DotNetType { get; }
	}
}
