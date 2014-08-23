using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Core.Config
{
	[DataContract]
	public class ConnectionInfo
	{
		[DataMember] public string DisplayName { get; set; }
		[DataMember] public string ConnectionString { get; set; }
		[DataMember] public string ConnectionType { get; set; }
	}
}
