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
	public class StoredProcMapping : ConfigBase<StoredProcMapping>
	{
		public StoredProcMapping(StoredProcedure definition)
		{
			Definiton = definition;
			FriendlyName = definition.Name.Replace("sp_", "");
		}

		[DataMember]
		public int ObjectID { get { return Definiton.Id; }}

		[DataMember]
		public string FriendlyName { get; set;}

		[DataMember]
		public string OutputType { get; set; }

		[DataMember]
		public string AssociatedType { get; set; }


		[DataMember]
		public string InputType { get; set; }

		[DataMember]
		public StoredProcedure Definiton { get; set; }

		[DataMember]
		public List<string> SystemFlags { get; set;}

		[DataMember]
		public List<string> UserFlags { get; set; }
	}
}
