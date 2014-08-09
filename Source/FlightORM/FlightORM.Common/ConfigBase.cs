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
				Save(str);
				File.WriteAllBytes(filename, str.ToArray());
			}
		}

		public void Save(Stream stream)
		{
			var ser = new DataContractJsonSerializer(typeof(T));
			ser.WriteObject(stream, (T)this);
		}

		public static T Load(string filename)
		{
			using(var str = new FileStream(filename, FileMode.Open))
			{
				return Load(str);
			}
		}

		public static T Load(Stream stream)
		{
				T result;
				var ser = new DataContractJsonSerializer(typeof(T));
				result = (T)ser.ReadObject(stream);
				result.OnDeserialization();
				return result;
		}

		protected virtual void OnDeserialization()
		{

		}
	}
}
