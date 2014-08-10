using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.SqlServer
{
	internal static class Helpers
	{
		public static Dictionary<string, int> GetColumnLookup(this SqlDataReader reader)
		{
			var result = new Dictionary<string, int>();
			for(int i=0;i<reader.FieldCount;i++)
			{
			   result.Add(reader.GetName(i), i);
			}
			return result;
		}
	}
}
