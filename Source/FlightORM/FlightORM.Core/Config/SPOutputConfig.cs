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
	public class SPOutputConfig
	{
		[DataMember]
		SPResult _definition;

		public SPOutputConfig(SPResult spResult)
		{
			_definition = spResult;
			Columns = _definition
				.Columns
				.Select(λ => new OutputColumnConfig(λ))
				.ToList();
		}

		#region WrappedProperties
		public int ResultIndex
		{
			get { return _definition.ResultIndex; }
		}

		public IList<OutputColumnConfig> Columns
		{
			get; private set;
		}

		public bool IsScalar
		{
			get { return _definition.IsProbablyScalar; }
		}

		public bool IsAction
		{
			get { return _definition.IsProbablyAction; }
		} 
		#endregion

	}
}
