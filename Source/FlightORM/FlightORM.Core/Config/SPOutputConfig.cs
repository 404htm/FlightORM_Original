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
	public class SPOutputConfig
	{
		public SPOutputConfig(SPResult spResult)
		{
			Definition = spResult;
			//Name = spResult.
		}

		[DataMember]
		public SPResult Definition { get; set;}
	}
}
