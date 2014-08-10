using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Common
{
	public class DbTypeInfo
	{
		public string TypeName { get; set; }
		public bool? AllowNull { get; set; }
		public int MaxLength { get; set; }
		public int Precision { get; set; }
		public int Scale { get; set; }
	}
}
