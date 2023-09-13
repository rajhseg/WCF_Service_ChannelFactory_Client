using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CalculatorLibrary;

namespace ComputerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    [GlobalErrorBehaviour(typeof(GlobalErrorHandler))]
    public class ComService : ICalculatorService, IFileService
    {
        public CalResponse Add(CalRequest req)
        {
            int result = 0;
            try
            {
                result = req.A + req.B;
            }
            catch(Exception ex)
            {
                throw new FaultException<CalFaultMessage>(new CalFaultMessage { Error = ex.Message });
            }
            return new CalResponse { Result = result };
        }

        public void StartProcess(Person per)
        {
            try
            {
                string filepath = @"F:\video\nature.mp4";
                var file = new FileInfo(filepath);
                var buffersize = 1024;
                var buffer = new byte[buffersize];                

                using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {                    
                    int bytesRead;
                    while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        Thread.Sleep(400);
                        var callb = OperationContext.Current.GetCallbackChannel<IFileCallBackService>();
                        callb.Progress(buffer);
                    }
                }
            }
            catch(Exception ex)
            {
                throw new FaultException<CalFaultMessage>(new CalFaultMessage { Error = ex.Message }, new FaultReason("l"));
            }
        }

        public CalResponse Subtract(CalRequest req)
        {

            int result = 0;
            try
            {
                result = req.A - req.B;
            }
            catch (Exception ex)
            {
                throw new FaultException<CalFaultMessage>(new CalFaultMessage { Error = ex.Message });
            }
            return new CalResponse { Result = result };
        }
    }
}
