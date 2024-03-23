using CalculatorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ComputerClient
{
    internal class ChannelFactorySample :  CalculatorLibrary.IFileCallBackService
    {
    	ChannelFactory<CalculatorLibrary.ICalculatorService> factory = null;
    	ICalculatorService channel = null;
    	
    	public ChannelFactorySample()
    	{
    		factory = new ChannelFactory<CalculatorLibrary.ICalculatorService>(
	              new WSHttpBinding(), new EndpointAddress("http://localhost:8082/ComService"));
    	}
    	
        public void Process()
        {
            /* Channel Factory */
			
            try {
	
	            channel = factory.CreateChannel();
	            var result = channel.Add(new CalculatorLibrary.CalRequest { A = 20, B = 2, Key = "sssddd" });	
	            Console.WriteLine(result.Result);
            }
            catch(System.ServiceModel.CommunicationException ex){
            	            	            		
            	if(factory.State == CommunicationState.Faulted){
            		
            		 factory = new ChannelFactory<CalculatorLibrary.ICalculatorService>(
	              		new WSHttpBinding(), new EndpointAddress("http://localhost:8082/ComService"));
            	}
            	
            } catch(Exception ex){
            	
            	 factory = new ChannelFactory<CalculatorLibrary.ICalculatorService>(
	              new WSHttpBinding(), new EndpointAddress("http://localhost:8082/ComService"));
            	
            }

            try
            {  
                /* Duplex channel Factory */
                InstanceContext context = new InstanceContext(this);
                var binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.None;

                DuplexChannelFactory<CalculatorLibrary.IFileService> factory2 = new DuplexChannelFactory<CalculatorLibrary.IFileService>(context,
                    binding, new EndpointAddress("net.tcp://localhost:8090/ComService"));

                var channel2 = factory2.CreateChannel();
                channel2.StartProcess(new Employee { EmployeeID = 1, Job = "AA", Name = "SD" });

                ((IClientChannel)channel2).Faulted += Program_Faulted;
            }
            catch (FaultException<CalculatorLibrary.CalFaultMessage> mes)
            {
                Console.WriteLine(mes.Message);
            }
            Console.Read();
        }

        private static void Program_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("error");
        }

        /* Callback Method from server */
        public void Progress(byte[] data)
        {
            Console.WriteLine(data.Length + ",");
        }
    }
}
