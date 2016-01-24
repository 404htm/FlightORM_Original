using FlightORM.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.UI
{
	public static class DesignerData
	{

		static StoredProcGroupVM _vm;
		public static StoredProcGroupVM StoredProcsVM
		{
			get {  
				if (_vm == null)
				{
					_vm = new StoredProcGroupVM(@"Data Source=.\dev;Initial Catalog=AdventureWorks2012;Integrated Security=True");
					_vm.Load();
				}
				return _vm;
			}
			
		}
	}
}
