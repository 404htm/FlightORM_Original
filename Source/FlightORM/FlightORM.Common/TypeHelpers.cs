using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Core
{
	public static class TypeHelpers
	{
		public static object ConvertToType(string value, string type)
		{
			switch (type)
			{
				case "Object": return (Object)value;
				case "Byte[]": return Convert.FromBase64String(value.Trim());
				//TODO: Figure out other primary types not part of typecode
			}

			var code = (TypeCode)Enum.Parse(typeof(TypeCode), type);
			return Convert.ChangeType(value, code);

		}
	}
}
