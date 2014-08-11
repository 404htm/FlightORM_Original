using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Common
{
	public class ResultSchema
	{
		public ResultSchema()
		{
			Columns = new List<ResultColumn>();
		}	
		//todo: source
		public int ResultIndex { get; set; }
		public List<ResultColumn> Columns { get; private set;}
		public bool IsScalar { get {  return Columns.Count() == 1; } }
		public bool IsAction { get {  return Columns.Count() == 0; } }
	}
}
