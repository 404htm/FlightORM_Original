﻿using FlightORM.Core;
using FlightORM.Core.Config;
using FlightORM.Core.Factories;
using FlightORM.SqlServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightORM.Common;

namespace FlightORM.UI.Models
{
	public class StoredProcGroupVM
	{
		ConnectionInfo _cnn;
		SPGroupConfig _group;

		public StoredProcGroupVM()
		{
			var cstr = @"Data Source=.\dev;Initial Catalog=NORTHWND;Integrated Security=True";
			_cnn = new ConnectionInfo{DisplayName="Default", ConnectionType = "sqlserver", ConnectionString=cstr};
			Load();
		}

		public StoredProcGroupVM(string connectionString)
		{
			_cnn = new ConnectionInfo { DisplayName = "Default", ConnectionType = "sqlserver", ConnectionString = connectionString };
			Load();
		}

		public void Load()
		{
			var loader = SPLoaderFactory.GetInstance(_cnn);
			var map = TypeMap.Load(@"C:\Users\Kelly Gendron\Source\Repos\FlightORM\Source\FlightORM\FlightORM.SqlServer\Defaults\default.typemap");
			_group = SPGroupConfig.CreateNew("Default", loader, map);
			var procs = _group.Procedures.Select(λ => new StoredProcVM(λ, loader)).ToList();
			Mappings = new ObservableCollection<StoredProcVM>(procs);
		}

		public ObservableCollection<StoredProcVM> Mappings { get; private set; }
	}
}
