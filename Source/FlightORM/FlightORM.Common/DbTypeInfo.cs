using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Common
{
	[DataContract]
	public class DbTypeInfo
	{
		public DbTypeInfo(string typeName, bool? allowNull, int maxLength, int precision, int scale)
		{
			TypeName = TypeName;
			AllowNull = allowNull;
			MaxLength = maxLength;
			Precision = precision;
			Scale = scale;
		}

		[DataMember] public string TypeName { get; private set; }
		[DataMember] public bool? AllowNull { get; private set; }
		[DataMember] public int MaxLength { get; private set; }
		[DataMember] public int Precision { get; private set; }
		[DataMember] public int Scale { get; private set; }
	}
}
