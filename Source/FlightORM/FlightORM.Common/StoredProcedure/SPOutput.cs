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
			Columns = new List<OutputColumn>();
			ResultIndex = index;
			DateRetrieved = DateTime.UtcNow;
		}	
		
		[DataMember] public int ResultIndex { get; private set; }
		[DataMember] public List<OutputColumn> Columns { get; private set;}
		[DataMember] public DateTime DateRetrieved { get; private set;}

		/// <summary>Query returns one result making it likely that result type should be scalar </summary>
		public bool IsProbablyScalar { get {  return Columns.Count() == 1; } }

		/// <summary>Query returns no results making it likely that result type should be void </summary>
		public bool IsProbablyAction { get {  return Columns.Count() == 0; } }
	}
}
