using FlightORM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FlightORM.Core.Config
{
	[DataContract]
	public class GlobalSettings : ConfigBase<GlobalSettings>
	{
		/// <summary>
		/// List of database names and their associated connection string (or name)
		/// </summary>
		[DataMember]
		public Dictionary<string, string> Connections { get; set; }

		[DataMember]
		public Dictionary<string, string> ProjectLocations { get; set; }


	}
}
