//## FILENAME: [ClassName]InputDo.cs
//## LOCATION: ...
//## AppliesTo: Classes in SPOutputClasses
//## GROUPSET: TYPESET, USESET

//## TYPESET.Lazy = ALL WHERE USERFLAGS.CONTAINS('Lazy')

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlightORM.Core.Templates.StoredProcedure
{
	public class ClassNameInputDo
	{
		//# FOREACH PROP(ALL)
		Type _propertyName;
		//# END FOREACH

		//# FOREACH PROP(TYPESET.Other)
		public Type PropertyName
		{
			get { return _propertyName; }
			set { _propertyName = value; }
		}
		//# END FOREACH
	}
}
