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
		public SPConfig(StoredProcedure definition)
		{
			Definition = definition;
			//TODO: Word Splitting Logic
			FriendlyName = definition.Name.Replace("sp_", "").Replace("_"," ");
			Enabled = true;
			Definition = definition;
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
		public StoredProcedure Definition { get; set; }

		[DataMember]
		public List<string> SystemFlags { get; set;}

		[DataMember]
		public List<string> UserFlags { get; set; }
	}
}
