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
			FriendlyName = NamingHelpers.SplitObjectName(Definition.Name);
			IsRequired = !Definition.HasDefault;
			Type=Definition.TypeInfo.TypeName;
			SampleValue = Definition.DefaultValue as String;
			Enabled = true;
		}

		[DataMember]
		public bool Enabled { get; set; }


		[DataMember]
		public SPParameter Definition {get; set;}

		[DataMember]
		public String FriendlyName { get; set; }

		[DataMember]
		public String Description { get; set; }

		[DataMember]
		public bool IsRequired { get; set;}

		[DataMember]
		public string Type { get; set;}

		[DataMember]
		public string SampleValue { get; set;}
	}
}
