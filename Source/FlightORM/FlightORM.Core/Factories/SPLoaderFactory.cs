using FlightORM.Common;
using FlightORM.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Core.Factories
{
	public class SPLoaderFactory
	{
		//TODO: Replace this with MEF so Core doesn't need to know about the providers
		public static ISPLoader GetInstance(ConnectionInfo connection)
		{
			switch(connection.ConnectionType.ToLowerInvariant())
			{
				case "sqlserver": return new SqlServer.SPLoader(connection.ConnectionString);
				default: throw new NotImplementedException();
			}
		}
	}
}
