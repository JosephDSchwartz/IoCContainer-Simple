using IoCContainer.Common.Enums;
using IoCContainer.Common.Models;
using System;
using System.Collections.Generic;

namespace IoCContainer.Service.Models
{
    public class Container : IContainer
    {
        private Dictionary<Type, Tuple<Type, LifetimeType>> typeResolver;
        private Dictionary<Type, Tuple<object, LifetimeType>> existingImplementations;

        public Container()
        {
            typeResolver = new Dictionary<Type, Tuple<Type, LifetimeType>>();
            existingImplementations = new Dictionary<Type, Tuple<object, LifetimeType>>();
        }

        public System.Collections.Generic.Dictionary<Type, Tuple<Type, LifetimeType>> TypeResolver
        {
            get { return typeResolver; }
        }
        public Dictionary<Type, Tuple<object, LifetimeType>> ExistingImplementations
        {
            get { return existingImplementations; }
        }
    }
}
