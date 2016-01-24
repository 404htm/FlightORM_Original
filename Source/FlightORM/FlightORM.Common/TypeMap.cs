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

		[DataMember] public List<TypeInfo> Entries { get; set; }
		[DataMember] public String Source { get; set;}

		public string GetCodeType(string dbType)
		{
			if(dbType == null) return null;

			var type = Entries
			.Where(λ => λ.DbType.Equals(dbType))
			.Select(λ => λ.CodeType)
			.FirstOrDefault();
			return type??"object";
		}
	}


}
