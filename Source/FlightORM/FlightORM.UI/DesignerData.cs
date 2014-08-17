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

		static StoredProcsVM _vm;
		public static StoredProcsVM StoredProcsVM
		{
			get {  
				if (_vm == null)
				{
					_vm = new StoredProcsVM(@"Data Source=.\dev;Initial Catalog=AdventureWorks2012;Integrated Security=True");
					_vm.Load();
				}
				return _vm;
			}
			
		}
	}
}
