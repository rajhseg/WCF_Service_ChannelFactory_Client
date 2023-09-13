using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ComputerService
{
    public class GlobalErrorBehaviourAttribute : Attribute, IServiceBehavior
    {
        Type errorHandlerType;

        public GlobalErrorBehaviourAttribute(Type errorHandlerType)
        {
            this.errorHandlerType = errorHandlerType;   
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var handler = (IErrorHandler)Activator.CreateInstance(errorHandlerType);

            foreach (var item in serviceHostBase.ChannelDispatchers)
            {
                var cp = item as ChannelDispatcher;
                cp.ErrorHandlers.Add(handler);
            }
        }
    }
}
