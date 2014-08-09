using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightORM.Common;
using System.Linq;

namespace FlightORM.CommonTests
{
	[TestClass]
	public class TypeMapTests
	{
		[TestMethod]
		public void LoadFile()
		{
			var str = Helpers.GetInputFile("sqlserver_typemap1.json");
			var map = TypeMap.Load(str);

			Assert.IsNotNull(map);
			Assert.IsTrue(map.Entries.Any());
			Assert.IsNotNull(map.Source);
		}
	}
}
