using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlightORM.Common
{
    [DataContract]
    public class TypeMapping
    {
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public bool IsValueType { get; set; }
        [DataMember]
        public string DbType { get; set; }
        [DataMember]
        public string FromDBMethod { get; set; }
        [DataMember]
        public string ToDBMethod { get; set; }

    }
}
