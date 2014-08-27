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
			Definiton = definition;
			DisplayName = Definiton.Name;
		}

		[DataMember] public bool Enabled { get; set;}
		[DataMember] public OutputColumn Definiton { get; private set; }
		[DataMember] public string DisplayName { get; private set; }
	}
}
