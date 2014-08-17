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
	public class SPGroupConfig : ConfigBase<SPGroupConfig>
	{
		[DataMember]
		public string Name { get; set;}

		[DataMember]
		public List<SPConfig> Procedures { get; set; }

		[DataMember]
		public string ConnectionName { get; set;}

		internal List<string> GetOutputClassDefinitions()
		{
			return null;
		}
	}

	 
}
