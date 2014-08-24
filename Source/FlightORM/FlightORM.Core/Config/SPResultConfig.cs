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
	public class SPResultConfig
	{
		[DataMember]
		SPResult _definition;

		public SPResultConfig(SPResult spResult)
		{
			_definition = spResult;
		}

		#region WrappedProperties
		public int ResultIndex
		{
			get { return _definition.ResultIndex; }
		}

		public IList<FlightORM.Common.ResultElement> Columns
		{
			get { return _definition.Columns; }
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
