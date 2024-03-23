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
    	
    	DuplexChannelFactory<CalculatorLibrary.IFileService> factory2 = null;
    	IFileService channel2 = null;
    	
    	public ChannelFactorySample()
    	{
    		// Channel Factory
    		factory = new ChannelFactory<CalculatorLibrary.ICalculatorService>(
	              new WSHttpBinding(), new EndpointAddress("http://localhost:8082/ComService"));    		
    		channel = factory.CreateChannel();
    		((IClientChannel)channel).Faulted+= new EventHandler(channel_Faulted);
    		
    		
    		// Duplex channel factory
    		InstanceContext context = new InstanceContext(this);
                var binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.None;
                
            factory2 = new DuplexChannelFactory<CalculatorLibrary.IFileService>(context,
                    binding, new EndpointAddress("net.tcp://localhost:8090/ComService"));

            channel2 = factory2.CreateChannel();
            ((IClientChannel)channel2).Faulted += Program_Faulted;
    	}

    	
    	void channel_Faulted(object sender, EventArgs e)
    	{
    		var closeChannel = (ICommunicationObject) channel;
    		
    		if(closeChannel!=null)
    			closeChannel.Close();
    		
    		factory = new ChannelFactory<CalculatorLibrary.ICalculatorService>(
	              		new WSHttpBinding(), new EndpointAddress("http://localhost:8082/ComService"));
            		
            channel = factory.CreateChannel();
            ((IClientChannel)channel).Faulted+= new EventHandler(channel_Faulted);
    	}
    	
    	
        private void Program_Faulted(object sender, EventArgs e)
        {
                		
        	var closeChannel = (ICommunicationObject) channel2;
    		
    		if(closeChannel!=null)
    			closeChannel.Close();
    		
    		InstanceContext context = new InstanceContext(this);
                var binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.None;
                
            
            factory2 = new DuplexChannelFactory<CalculatorLibrary.IFileService>(context,
                    binding, new EndpointAddress("net.tcp://localhost:8090/ComService"));

            channel2 = factory2.CreateChannel();
			((IClientChannel)channel2).Faulted += Program_Faulted;
        }

    	
        public void Process()
        {
            /* Channel Factory */			
            try {		            
	            var result = channel.Add(new CalculatorLibrary.CalRequest { A = 20, B = 2, Key = "sssddd" });	
	            Console.WriteLine(result.Result);
            }
            catch(FaultException ex){
            	
            	Console.WriteLine(ex.Message);
            }

            try
            {  
                /* Duplex channel Factory */
                channel2.StartProcess(new Employee { EmployeeID = 1, Job = "AA", Name = "SD" });                
            }
            catch (FaultException<CalculatorLibrary.CalFaultMessage> mes)
            {
                Console.WriteLine(mes.Message);
            }
            Console.Read();
        }
        
        /* Callback Method from server */
        public void Progress(byte[] data)
        {
            Console.WriteLine(data.Length + ",");
        }
    }
}
