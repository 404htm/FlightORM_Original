using FlightORM.Core.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightORM.Common;

namespace FlightORM.UI.Models
{
	
	public class StoredProcVM : SPConfig
	{
		IStoredProcAnalyzer _spAnalyzer;
		SPConfig _config;

		internal StoredProcVM(SPConfig config, IStoredProcAnalyzer spAnalyzer )
		{
			_config = config;
			_spAnalyzer = spAnalyzer;

		}

		public new int ObjectId { get { return _config.ObjectId; } }
		public new bool Enabled {
			get { return _config.Enabled;}
			set { _config.Enabled = value; }
		}

		public new string FriendlyName
		{
			get { return _config.FriendlyName; }
			set { _config.FriendlyName = value; }
		}
		public new string OutputType
		{
			get { return _config.OutputType; }
			set { _config.OutputType = value; }
		}
		public new string AssociatedType
		{
			get { return _config.AssociatedType; }
			set { _config.AssociatedType= value; }
		}
		public new string InputType
		{
			get { return _config.InputType; }
			set { _config.InputType = value; }
		}
		public new SPInfo Definition
		{
			get { return _config.Definition; }
			set { _config.Definition = value; }
		}
		public new IList<string> SystemFlags
		{
			get { return _config.SystemFlags; }
			set { _config.SystemFlags = value; }
		}
		public new IList<string> UserFlags
		{
			get { return _config.UserFlags; }
			set { _config.UserFlags = value; }
		}

		public new IList<SPParameterConfig> Parameters
		{
			get { return _config.Parameters; }
			set { _config.Parameters = value; }
		}

		public void Run()
		{
			var values = new Dictionary<string, string>();
			foreach(var el in this.Parameters)
			{
				values.Add(el.Definition.Name, el.SampleValue);
			}

			this._spAnalyzer.LoadOutputSchema(this.Definition, values, true);

		}

		public bool TestProc()
		{
			
			//_spAnalyzer.LoadOutputSchema(_config.Definition);
			return false;
		}
	}
}
