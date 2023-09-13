using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{

    [KnownType(typeof(Employee))]
    [KnownType(typeof(Laymen))]
    [DataContract]
    public class Person
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Job { get; set; }
    }

    [DataContract]
    public class Employee : Person
    {
        [DataMember(IsRequired =false)]
        public int EmployeeID { get; set; } 
    }

    [DataContract]
    public class Laymen :Person
    {
        [DataMember]
        public string LaymenId { get; set; }
    }

    [MessageContract]
    public class CalRequest
    {
        [MessageHeader]
        public string Key { get; set; }

        [MessageBodyMember(Order =1)]
        public int A { get; set; }

        [MessageBodyMember(Order =2)]
        public int B { get; set; }
    }

    [MessageContract]
    public class CalResponse
    {
        [MessageBodyMember]
        public int Result { get; set; }
    }

    [DataContract]
    public class CalFaultMessage
    {
        [DataMember]
        public string Error { get; set; }
    }
}
