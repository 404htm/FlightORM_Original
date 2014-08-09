using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.CommonTests
{
	public static class Helpers
	{
		public static Stream GetInputFile(string filename)
		{
			Assembly thisAssembly = Assembly.GetExecutingAssembly();
			string path = "FlightORM.CommonTests.TestFiles";
			return thisAssembly.GetManifestResourceStream(path + "." + filename);
		} 
	}
}
