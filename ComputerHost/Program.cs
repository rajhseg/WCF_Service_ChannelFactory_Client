﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ComputerHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ComputerService.ComService));
            host.Open();
            Console.WriteLine("Service hosted");
            Console.Read();
        }
    }
}
