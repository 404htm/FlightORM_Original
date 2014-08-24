using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Common
{
	[DataContract]
	public class SPInfo
	{
		public SPInfo(int id, string name, string schema, DateTime created, DateTime modified)
		{
			Id = id;
			Name = name;
			Schema = schema;
			DateCreated = created;
			DateModified = modified;
		}

		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Schema { get; private set;}
		public DateTime DateCreated {get; private set;}
		public DateTime DateModified {get; private set;}

	}
}
