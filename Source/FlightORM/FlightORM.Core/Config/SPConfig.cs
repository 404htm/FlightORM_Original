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
	public class SPConfig 
	{
		public SPConfig()
		{

		}
		public SPConfig(SPInfo definition)
		{
			Definition = definition;
			FriendlyName = NamingHelpers.SplitObjectName(definition.Name);
			Enabled = true;
			Definition = definition;
			Parameters = definition.InputParameters.Select(p => new SPParameterConfig(p)).ToList();
		}

		[DataMember]
		public int ObjectId { get { return Definition.Id; }}

		[DataMember]
		public bool Enabled { get; set;}

		[DataMember]
		public string FriendlyName { get; set;}

		[DataMember]
		public string OutputType { get; set; }

		[DataMember]
		public string AssociatedType { get; set; }


		[DataMember]
		public string InputType { get; set; }

		[DataMember]
		public SPInfo Definition { get; set; }

		[DataMember]
		public IList<string> SystemFlags { get; set;}

		[DataMember]
		public IList<string> UserFlags { get; set; }

		[DataMember]
		public IList<SPParameterConfig> Parameters { get; set; }

		[DataMember]
		public IList<SPResultConfig> Results { get; set; }
	}
}
