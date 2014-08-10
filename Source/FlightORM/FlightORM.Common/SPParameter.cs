using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightORM.Common
{
	public class SPParameter
	{
		public DbTypeInfo TypeInfo { get; set;}
		public string Name { get; set;}
		public int Position { get; set; }
		public bool IsOutput { get; set; }
		public bool IsReadOnly { get; set;}
		public bool HasDefault { get; set;}
		public object DefaultValue { get; set;}
	}
}
