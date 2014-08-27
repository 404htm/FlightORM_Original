using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FlightORM.Common
{
	[DataContract]
	public class OutputColumn
	{
		public OutputColumn(Type type, string name)
		{
			Type = type;
			Name = name;
		}

		[DataMember] public Type Type { get; private set; }
		[DataMember] public string Name { get; private set; }
	}
}
