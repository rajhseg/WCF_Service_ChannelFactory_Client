using System.Runtime.Serialization;
using System.ServiceModel;

namespace CalculatorLibrary
{
    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        [FaultContract(typeof(CalFaultMessage))]
        CalResponse Add(CalRequest req);

        [OperationContract(IsOneWay = false)]
        [FaultContract(typeof(CalFaultMessage))]
        CalResponse Subtract(CalRequest req);
    }

    [ServiceContract(CallbackContract =typeof(IFileCallBackService), SessionMode =SessionMode.Required)]
    public interface IFileService
    {
        [OperationContract(IsOneWay = true)]        
        void StartProcess(Person per);
    }

    [ServiceContract]
    public interface IFileCallBackService
    {
        [OperationContract(IsOneWay =true)]
        void Progress(byte[] data);
    }
}