using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using FlightORM.SqlServer;
using System.Linq;
using FlightORM.SqlServerTests.Properties;
using System.Data.SqlClient;

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

		[TestMethod]
		public void LoadOutputStructure_Adventure()
		{
			var spa = new StoredProcAnalysis(Settings.Default.AdventureWorks);
			var proc = spa.GetProcedures().Where(p => p.Name == "uspGetBillOfMaterials").Single();

			var cmd = new SqlCommand("uspGetBillOfMaterials");
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.Parameters.Add(new SqlParameter("@StartProductID", 893));
			cmd.Parameters.Add(new SqlParameter("@CheckDate", new DateTime(2004, 4, 18)));
			spa.LoadOutputSchema(proc, cmd);

			Assert.IsTrue(proc.OutputData != null);
			Assert.IsTrue(proc.OutputData.FirstOrDefault() != null);
			Assert.IsTrue(proc.OutputData.First().Columns.Count() ==  8);
		}



	}
}
