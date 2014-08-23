using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Core.Config
{
	public class ConnectionInfo
	{
		public string DisplayName { get; set; }
		public string ConnectionString { get; set;}
		public string ConnectionType { get; set;}
	}
}
