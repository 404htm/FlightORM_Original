using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Core
{
	public static class NamingHelpers
	{
		/// <summary>
		/// Takes an object name and splits it in to multiple words
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string SplitObjectName(string name)
		{
			//TODO: Complete logic - this is just a stub
			if(name.Contains('_')) return name.Replace('_',' ');
			return name;

		}

	}
}
