using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Common
{
	[DataContract]
	public abstract class ConfigBase<T> where T : ConfigBase<T>
	{
		public void Save(string filename)
		{
			using (var str = new MemoryStream())
			{
				var ser = new DataContractJsonSerializer(typeof(T));
				ser.WriteObject(str, (T)this);
				File.WriteAllBytes(filename, str.ToArray());
			}
		}

		public static T Load(string filename)
		{
			var ser = new DataContractJsonSerializer(typeof(T));
			using(var str = new FileStream(filename, FileMode.Open))
			{
				T result;
				result = (T)ser.ReadObject(str);
				result.OnDeserialization();
				return result;
			}
			
		}

		protected virtual void OnDeserialization()
		{

		}
	}
}
