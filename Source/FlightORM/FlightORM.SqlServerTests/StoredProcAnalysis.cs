using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using FlightORM.SqlServer;
using System.Linq;
using FlightORM.SqlServerTests.Properties;

namespace FlightORM.SqlServerTests
{
	[TestClass]
	public class StoredProcAnalysisTests
	{

		[TestMethod]
		public void LoadSp_Northwind()
		{
			var spa = new StoredProcAnalysis(Settings.Default.Northwind);
			var procs = spa.GetProcedures();

			Assert.IsTrue(procs.Any());
		}

		[TestMethod]
		public void LoadSp_Adventure()
		{
			var spa = new StoredProcAnalysis(Settings.Default.AdventureWorks);
			var procs = spa.GetProcedures();

			Assert.IsTrue(procs.Any());
		}

		[TestMethod]
		public void LoadParams_individual_Northwind()
		{
			var spa = new StoredProcAnalysis(Settings.Default.Northwind);
			var procs = spa.GetProcedures().ToList();
			foreach(var p in procs) spa.LoadParameters(p);

			Assert.IsTrue(! procs.Where(p => p.InputParameters == null).Any());
			Assert.IsTrue(procs.Where(p => p.InputParameters.Any()).Any());
		}

		[TestMethod]
		public void LoadParams_individual_Adventure()
		{
			var spa = new StoredProcAnalysis(Settings.Default.AdventureWorks);
			var procs = spa.GetProcedures().ToList();
			foreach (var p in procs) spa.LoadParameters(p);

			Assert.IsTrue(!procs.Where(p => p.InputParameters == null).Any());
			Assert.IsTrue(procs.Where(p => p.InputParameters.Any()).Any());
		}
	}
}
