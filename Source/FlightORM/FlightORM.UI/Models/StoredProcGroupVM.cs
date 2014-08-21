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
	public class StoredProcGroupVM
	{
		string _cnn;

		/// <summary>
		/// Designer Constructor
		/// </summary>
		public StoredProcGroupVM()
		{
			_cnn = @"Data Source=.\dev;Initial Catalog=NORTHWND;Integrated Security=True";
			Load();
		}

		public StoredProcGroupVM(string connectionString)
		{
			_cnn=connectionString;
			
		}

		public void Load()
		{
			var loader = new StoredProcAnalysis(_cnn);
			var procs = loader.GetProcedures().ToList();
			foreach (var p in procs) loader.LoadParameters(p);

			var items = procs.Select(p => new SPConfig(p)).Select(p => new StoredProcVM(p, loader)).ToList();
			Mappings = new ObservableCollection<StoredProcVM>(items);
		}

		public ObservableCollection<StoredProcVM> Mappings { get; private set; }
	}
}
