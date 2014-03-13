using IoCContainer.Common.Enums;
using System;
using System.Collections.Generic;

namespace IoCContainer.Common.Models
{
    public interface IContainer
    {
        Dictionary<Type, Tuple<Type, LifetimeType>> TypeResolver { get; }
        Dictionary<Type, Tuple<object, LifetimeType>> ExistingImplementations { get; }
    }
}
