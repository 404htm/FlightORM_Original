using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FlightORM.Common;

namespace FlightORM.Core.Config
{
	[DataContract]
	public class OutputColumnConfig
	{
		public OutputColumnConfig(OutputColumn definition)
		{
			Definition = definition;

			Enabled = true;
			FriendlyName = definition.Name;
			Type = definition.Type;
		}

		[DataMember] public Type Type { get; set; }
		[DataMember] public bool Enabled { get; set;}
		[DataMember] public OutputColumn Definition { get; private set; }
		[DataMember] public string FriendlyName { get; private set; }
	}
}
