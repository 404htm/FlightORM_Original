﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Common
{
	public class SPInfo
	{
		public SPInfo()
		{

		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string Schema { get; set;}
		public DateTime DateCreated {get;set;}
		public DateTime DateModified {get;set;}
		public List<SPParameter> InputParameters {get; set;}
		public List<SPResult> OutputData { get; set;}

		public bool? IsValid { get; set; }
		public string Error { get; set; }
	}
}