using CalculatorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ComputerClient
{    
    internal class Program
    {
        static void Main(string[] args)
        {
            // WcfClientSample wcfClientSample = new WcfClientSample();
            // wcfClientSample.Process();

            ChannelFactorySample channelFactorySample = new ChannelFactorySample();
            channelFactorySample.Process();

            Console.Read();
        }
    }

}
