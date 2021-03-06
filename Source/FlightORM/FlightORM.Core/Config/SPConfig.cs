﻿using FlightORM.Common;
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
		TypeMap _typeMap;

		#endregion

		internal SPConfig(SPInfo definition, ISPLoader loader, TypeMap typeMap)
		{
			_core = definition;
			_loader = loader;
			_typeMap = typeMap;

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
		public IList<SPOutputConfig> Outputs { get; set; }

		public void LoadParameters()
		{
			var result = _loader.GetParameters(_core)
					.Select(λ => new SPParameterConfig(λ, _typeMap))
					.ToList();
			this.Parameters = result;
		}

		public bool LoadOutputs()
		{
			string msg;
			var result = _loader.GetOutputSchema(_core, this.Parameters.Cast<IParameterTestInfo>(), out msg);
			Outputs = result
				.Select(λ => new SPOutputConfig(λ))
				.ToList();

			var isValid = msg == null;
			this.IsValid = isValid;
			this.ErrorText = msg;
			return isValid;
		}
	}
}
