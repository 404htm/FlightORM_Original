using FlightORM.Common;
using FlightORM.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Core
{
	/// <summary>
	/// Class for storing and managing the current state of a group of stored procedures.
	/// This includes tracking database changes so the user can manually resolve conflicts.
	/// </summary>
	[DataContract]
	public class SPGroupConfig : ConfigBase<SPGroupConfig>, IEnumerable<SPConfig>
	{

		#region Storage Properties

		[DataMember] String _name;

		[DataMember] String _connectionName;

		[DataMember] IList<SPConfig> _items;

		#endregion

		#region Private Properties

		ISPLoader _loader;

		#endregion

		#region Public Properties

		public string Name 
		{
			get {  return _name; }
			set { _name = value; }
		}
		public IList<SPConfig> Procedures
		{
			get { return _items; }
		}

		#endregion	

		



		public void CreateNew(ISPLoader loader)
		{
			_loader = loader;
			_items = _loader.GetProcedures()
				.Select(λ => new SPConfig(λ))
				.ToList();
		}


		public void RescanAll()
		{
			throw new NotImplementedException();
		}

		public void ValidateAll()
		{
			throw new NotImplementedException();
		}

		public void AcceptChange(int Id)
		{
			throw new NotImplementedException();
		}

		public void IgnoreChange(int Id)
		{
			throw new NotImplementedException();
		}

		public void TransitionDatabase(ConnectionInfo newConnection)
		{
			throw new NotImplementedException();
		}


		#region IEnumerable Implementation
		public IEnumerator<SPConfig> GetEnumerator()
		{
			return _items.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		} 
		#endregion
	}
}
