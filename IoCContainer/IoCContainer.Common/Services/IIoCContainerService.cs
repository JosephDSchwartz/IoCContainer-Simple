
using IoCContainer.Common.Enums;
using System;
namespace IoCContainer.Common.Services
{
    public interface IIoCContainerService
    {
        IIoCContainerService Register<Interface, Implementation>(LifetimeType lifetimeType = LifetimeType.Default);
        T Resolve<T>();
        object Resolve(Type typeToResolve);
    }
}
