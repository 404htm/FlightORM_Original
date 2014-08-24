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
	
	public class StoredProcVM :  INotifyPropertyChanged
	{
		ISPLoader _spAnalyzer;
		SPConfig _config;

		internal StoredProcVM(SPConfig config, ISPLoader spAnalyzer )
		{
			_config = config;
			_spAnalyzer = spAnalyzer;
			//if(this.Definition.OutputData != null)
			//{
			//	this.Results = this.Definition.OutputData.Select(d => new SPResultConfig(d)).ToList();
			//}
			
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
		}

		public IList<SPResultConfig> Results
		{
			get; private set;
		}

		public void Run()
		{
			var values = new Dictionary<string, string>();
			foreach(var el in this.Parameters)
			{
				values.Add(el.Definition.Name, el.SampleValue);
			}

			//this._spAnalyzer.GetOutputSchema(this.Definition, values, true);
			//TODO: Design resolver that deals with change detection and restoration of current values
			//this.Results = this.Definition.OutputData.Select(d => new SPResultConfig(d)).ToList();
			onPropChanged("Results");
		}




		public event PropertyChangedEventHandler PropertyChanged;

		void onPropChanged(string propName)
		{
			if(PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	}
}
