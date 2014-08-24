using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Common
{
	[DataContract]
	public class SPResult
	{
		public SPResult(int index)
		{
			Columns = new List<ResultElement>();
			ResultIndex = index;
			DateRetrieved = DateTime.UtcNow;
		}	
		
		[DataMember] public int ResultIndex { get; private set; }
		[DataMember] public List<ResultElement> Columns { get; private set;}
		[DataMember] public DateTime DateRetrieved { get; private set;}

		public bool IsScalar { get {  return Columns.Count() == 1; } }
		public bool IsAction { get {  return Columns.Count() == 0; } }
	}
}
