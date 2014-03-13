using IoCContainer.Common.Enums;
using IoCContainer.Common.Models;
using IoCContainer.Common.Services;
using IoCContainer.Service.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace IoCContainer.Service.Services
{
    public class IoCContainerService : IIoCContainerService
    {
        private IContainer container;

        public IoCContainerService() : this(new Container())
        {

        }

        public IoCContainerService(IContainer container)
        {
            this.container = container;
        }

        internal IContainer Container { get { return container; } }

        public IIoCContainerService Register<Interface, Implementation>(LifetimeType lifetimeType = LifetimeType.Default)
        {
            container.TypeResolver.Add(typeof(Interface), Tuple.Create(typeof(Implementation), lifetimeType));

            return this;
        }

        public T Resolve<T>()
        {
            return (T)this.Resolve(typeof(T));
            //if (!container.TypeResolver.ContainsKey(typeof(T)))
            //{
            //    throw new ArgumentException("Could not resolve type: " + typeof(T).FullName);
            //}

            //return (T)ResolveByLifeTime(container.TypeResolver[typeof(T)]);
        }

        public object Resolve(Type typeToResolve)
        {
            if (!container.TypeResolver.ContainsKey(typeToResolve))
            {
                throw new ArgumentException("Could not resolve type: " + typeToResolve.FullName);
            }

            return ResolveByLifeTime(container.TypeResolver[typeToResolve]);
        }

        internal object ResolveByLifeTime(Tuple<Type, LifetimeType> typeToLifetimeType)
        {
            switch(typeToLifetimeType.Item2)
            {
                case LifetimeType.Default: 
                case LifetimeType.Transient:
                    return this.CreateType(typeToLifetimeType.Item1);
                case LifetimeType.Singleton:
                    if(!container.ExistingImplementations.ContainsKey(typeToLifetimeType.Item1))
                    {
                        container.ExistingImplementations.Add(typeToLifetimeType.Item1, Tuple.Create(this.CreateType(typeToLifetimeType.Item1), typeToLifetimeType.Item2));
                    }

                    return container.ExistingImplementations[typeToLifetimeType.Item1].Item1;
                default:
                    return null;
            }
        }

        internal object CreateType(Type typeToCreate)
        {
            return typeToCreate.GetConstructors().Any() ? CreateWithConstructorParameters(typeToCreate) : Activator.CreateInstance(typeToCreate);
        }

        internal object CreateWithConstructorParameters(Type typeToCreate)
        {
            var constructorInfos = typeToCreate.GetConstructors();
            var mostDescriptiveConstructor = FindMostDescriptiveConstructor(constructorInfos);

            if(mostDescriptiveConstructor != null && mostDescriptiveConstructor.GetParameters().Any())
            {
                var resolvedParameters = new Collection<object>();

                foreach(var parameter in mostDescriptiveConstructor.GetParameters())
                {
                    var parameterType = parameter.ParameterType;
                    resolvedParameters.Add(this.Resolve(parameterType));
                }

                return Activator.CreateInstance(typeToCreate, resolvedParameters.ToArray());
            }

            return Activator.CreateInstance(typeToCreate);
        }

        internal ConstructorInfo FindMostDescriptiveConstructor(IEnumerable<ConstructorInfo> constructors)
        {
            return constructors == null ? null : constructors.OrderByDescending(c => c.GetParameters().Count()).FirstOrDefault(c => c.GetParameters().All(p => container.TypeResolver.ContainsKey(p.ParameterType)));
        }
    }
}
