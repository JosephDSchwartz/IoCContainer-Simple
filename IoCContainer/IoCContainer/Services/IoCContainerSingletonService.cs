using IoCContainer.Common.Enums;
using IoCContainer.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCContainer.Service.Services
{
    public sealed class IoCContainerSingletonService : IIoCContainerService
    {
        private static volatile IoCContainerSingletonService instance;
        private static object avoidDeadlocks = new object();
        private IIoCContainerService containerService;

        private IoCContainerSingletonService() 
        {
            containerService = new IoCContainerService();
        }

        public static IoCContainerSingletonService Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (avoidDeadlocks)
                    {
                        if(instance == null)
                        {
                            instance = new IoCContainerSingletonService();
                        }
                    }
                }

                return instance;
            }
        }

        public IIoCContainerService Register<Interface, Implementation>(LifetimeType lifetimeType = LifetimeType.Default)
        {
            containerService.Register<Interface, Implementation>(lifetimeType);

            return Instance;
        }

        public T Resolve<T>()
        {
            return containerService.Resolve<T>();
        }


        public object Resolve(Type typeToResolve)
        {
            return containerService.Resolve(typeToResolve);
        }
    }
}
