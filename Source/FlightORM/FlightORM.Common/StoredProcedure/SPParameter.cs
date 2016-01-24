using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FlightORM.Common
{
	[DataContract]
	public class SPParameter
	{
		public SPParameter(string name, DbTypeInfo type, int position, bool isOutput, bool isReadOnly, object defaultValue)
		{
			Name = name;
			TypeInfo = type;
			Position = position;
			IsOutput = isOutput; 
			IsReadOnly = isReadOnly;
			DefaultValue = defaultValue;
		}

		[DataMember] public DbTypeInfo TypeInfo { get; private set;}
		[DataMember] public string Name { get; private set; }
		[DataMember] public int Position { get; private set; }
		[DataMember] public bool IsOutput { get; private set; }
		[DataMember] public bool IsReadOnly { get; private set; }
		[DataMember] public object DefaultValue { get; private set; }
	}
}
