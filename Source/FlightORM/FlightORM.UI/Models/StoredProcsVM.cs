using FlightORM.Core.Config;
using FlightORM.SqlServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.UI.Models
{
	public class StoredProcsVM
	{
		string _cnn;

		/// <summary>
		/// Designer Constructor
		/// </summary>
		public StoredProcsVM()
		{
			_cnn = @"Data Source=.\dev;Initial Catalog=AdventureWorks2012;Integrated Security=True";
			Load();
		}

		public StoredProcsVM(string connectionString)
		{
			_cnn=connectionString;
			
		}

		public void Load()
		{
			var loader = new StoredProcAnalysis(_cnn);
			var procs = loader.GetProcedures().ToList();
			foreach (var p in procs) loader.LoadParameters(p);

			var items = procs.Select(p => new StoredProcMapping(p)).ToList();
			Mappings = new ObservableCollection<StoredProcMapping>(items);
		}

		public ObservableCollection<StoredProcMapping> Mappings { get; private set; }
	}
}
