using FlightORM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Core.Config
{
	[DataContract]
	public class SPParameterConfig
	{
		public SPParameterConfig(SPParameter parameter)
		{
			Definition = parameter;
		}

		[DataMember]
		public SPParameter Definition {get; set;}

		[DataMember]
		public String FriendlyName { get; set; }

		[DataMember]
		public String Description { get; set; }

		[DataMember]
		public Boolean IsRequired { get; set;}

		[DataMember]
		public Type Type { get; set;}

	}
}
