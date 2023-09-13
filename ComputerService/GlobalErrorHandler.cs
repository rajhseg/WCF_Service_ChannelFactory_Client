using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ComputerService
{
    public class GlobalErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            FaultCode fc = new FaultCode("new fault code");
            fault = Message.CreateMessage(version, fc, "sample reason", "sample action");
        }
    }
}
