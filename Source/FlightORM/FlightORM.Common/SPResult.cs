using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Common
{
	public class SPResult
	{
		public SPResult()
		{
			Columns = new List<ResultElement>();
		}	
		//todo: source
		public int ResultIndex { get; set; }
		public List<ResultElement> Columns { get; private set;}
		public bool IsScalar { get {  return Columns.Count() == 1; } }
		public bool IsAction { get {  return Columns.Count() == 0; } }
	}
}
