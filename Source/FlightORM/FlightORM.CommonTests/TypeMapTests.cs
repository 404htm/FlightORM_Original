using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightORM.Common;
using System.Linq;
using System.IO;

namespace FlightORM.CommonTests
{
	[TestClass]
	public class TypeMapTests
	{
		[TestMethod]
		public void LoadEmbeddedFileV1()
		{
			var str = Helpers.GetInputFile("sqlserver_typemap1.json");
			var map = TypeMap.Load(str);

			Assert.IsNotNull(map);
			Assert.IsTrue(map.Entries.Any());
			Assert.IsNotNull(map.Source);
		}

		[TestMethod]
		public void TestSerializationAndDeserialization()
		{
			var map = new TypeMap();
			map.Entries.Add(new TypeInfo());
			map.Entries.Add(new TypeInfo());
			map.Source = "Test Source";

			var f = Path.GetTempFileName();
			map.Save(f);
			var map2 = TypeMap.Load(f);

			Assert.IsNotNull(map2);
			Assert.IsTrue(map.Source == map2.Source);
			Assert.IsTrue(map.Entries.Count == map2.Entries.Count());
		}
	}
}
