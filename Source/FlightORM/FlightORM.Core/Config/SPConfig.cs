using FlightORM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Core.Config
{
	[DataContract]
	public class SPConfig 
	{

		#region Storage Properties

		[DataMember] SPInfo _core;

		#endregion

		#region Private Properties

		ISPLoader _loader;

		#endregion

		internal SPConfig(SPInfo definition, ISPLoader loader)
		{
			_core = definition;
			_loader = loader;

			//Defaults:
			FriendlyName = NamingHelpers.SplitObjectName(definition.Name);
			Enabled = true;
		}

		#region Public Properties
			public SPInfo Definition { get { return _core; } }

		#endregion

		public bool? IsValid { get; private set;}
		public string ErrorText { get; private set;}

		[DataMember]
		public bool Enabled { get; set;}

		[DataMember]
		public string FriendlyName { get; set;}

		[DataMember]
		public string OutputType { get; set; }

		[DataMember]
		public string AssociatedType { get; set; }

		[DataMember]
		public string InputType { get; set; }

		[DataMember]
		public IList<string> SystemFlags { get; set;}

		[DataMember]
		public IList<string> UserFlags { get; set; }

		[DataMember]
		public IList<SPParameterConfig> Parameters { get; private set; }

		[DataMember]
		public IList<SPResultConfig> Results { get; set; }

		public void LoadParameters()
		{
			var result = _loader.GetParameters(_core)
					.Select(λ => new SPParameterConfig(λ))
					.ToList();
			this.Parameters = result;
		}

		public void LoadOutputs()
		{
			string msg;
			var result = _loader.GetOutputSchema(_core, this.Parameters.Cast<IParameterTestInfo>(), out msg);
			this.IsValid = msg == null;
			this.ErrorText = msg;
		}
	}
}
