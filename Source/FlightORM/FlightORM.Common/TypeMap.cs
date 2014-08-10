using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Common
{
	[DataContract]
	public class TypeMap : ConfigBase<TypeMap>
	{
		public TypeMap()
		{
			Entries = new List<TypeInfo>();
		}

		[DataMember]
		public List<TypeInfo> Entries { get; set; }
		[DataMember]
		public String Source { get; set;}
	}


	[DataContract]
	public class TypeInfo
	{
	    [DataMember]
        public string CodeType { get; set; }
        [DataMember]
        public bool IsValueType { get; set; }
        [DataMember]
        public string DbType { get; set; }
        [DataMember]
        public string Accessor { get; set; }
		[DataMember]
        public string Enumeration { get; set; }
    }
}
