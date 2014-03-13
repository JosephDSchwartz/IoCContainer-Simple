using IoCContainer.Service.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCContainer.Service.Tests
{
    [TestFixture]
    public class IoCContainerServiceTests
    {
        #region Registration Tests

        [Test]
        public void Register_Type_With_Default_LifetimeType_Test()
        {
            var containerService = new IoCContainerService();

            containerService.Register<ITestTypeOne, TestTypeOne>();

            Assert.IsTrue(containerService.Container.TypeResolver.ContainsKey(typeof(ITestTypeOne)) 
                && containerService.Container.TypeResolver[typeof(ITestTypeOne)].Item2 == Common.Enums.LifetimeType.Default);
        }

        [Test]
        public void Register_Type_With_Transient_LifetimeType_Test()
        {
            var containerService = new IoCContainerService();

            containerService.Register<ITestTypeOne, TestTypeOne>(Common.Enums.LifetimeType.Transient);

            Assert.IsTrue(containerService.Container.TypeResolver.ContainsKey(typeof(ITestTypeOne)) 
                && containerService.Container.TypeResolver[typeof(ITestTypeOne)].Item2 == Common.Enums.LifetimeType.Transient);
        }

        [Test]
        public void Register_Type_With_Singleton_LifetimeType_Test()
        {
            var containerService = new IoCContainerService();

            containerService.Register<ITestTypeOne, TestTypeOne>(Common.Enums.LifetimeType.Singleton);

            Assert.IsTrue(containerService.Container.TypeResolver.ContainsKey(typeof(ITestTypeOne)) 
                && containerService.Container.TypeResolver[typeof(ITestTypeOne)].Item2 == Common.Enums.LifetimeType.Singleton);
        }

        #endregion

        #region Resolution Tests

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Resolve_Unknown_Type_Test()
        {
            var containerService = new IoCContainerService();

            containerService.Resolve<ITestTypeOne>();
        }

        [Test]
        public void Resolve_Default_Type_Test()
        {
            var containerService = new IoCContainerService();
            containerService.Register<ITestTypeOne, TestTypeOne>();
            
            var implementationOne = containerService.Resolve<ITestTypeOne>();
            var implementationTwo = containerService.Resolve<ITestTypeOne>();

            Assert.IsTrue(implementationOne is TestTypeOne && implementationTwo is TestTypeOne && implementationOne != implementationTwo);
        }

        [Test]
        public void Resolve_Transient_Type_Test()
        {
            var containerService = new IoCContainerService();
            containerService.Register<ITestTypeOne, TestTypeOne>(Common.Enums.LifetimeType.Transient);

            var implementationOne = containerService.Resolve<ITestTypeOne>();
            var implementationTwo = containerService.Resolve<ITestTypeOne>();

            Assert.IsTrue(implementationOne is TestTypeOne && implementationTwo is TestTypeOne && implementationOne != implementationTwo);
        }

        [Test]
        public void Resolve_Singleton_Type_Test()
        {
            var containerService = new IoCContainerService();
            containerService.Register<ITestTypeOne, TestTypeOne>(Common.Enums.LifetimeType.Singleton);

            var implementationOne = containerService.Resolve<ITestTypeOne>();
            var implementationTwo = containerService.Resolve<ITestTypeOne>();

            Assert.IsTrue(implementationOne is TestTypeOne && implementationTwo is TestTypeOne && implementationOne == implementationTwo);
        }

        [Test]
        public void Resolve_Type_With_Resolvable_Constructor_Arguements_Test()
        {
            var containerService = new IoCContainerService();
            containerService.Register<ITestTypeOne, TestTypeOne>();
            containerService.Register<ITestTypeWithParameterConstructor, TestTypeWithParameterConstructor>();

            var implementation = containerService.Resolve<ITestTypeWithParameterConstructor>();

            Assert.IsTrue(implementation is TestTypeWithParameterConstructor && implementation.TestTypeOne is TestTypeOne);
        }

        #endregion
    }

    interface ITestTypeOne { }

    interface ITestTypeWithParameterConstructor 
    {
        ITestTypeOne TestTypeOne { get; }
    }

    class TestTypeOne : ITestTypeOne { }

    class TestTypeWithParameterConstructor : ITestTypeWithParameterConstructor 
    {
        public ITestTypeOne TestTypeOne { get; private set; }

        public TestTypeWithParameterConstructor(ITestTypeOne testTypeOne) 
        {
            this.TestTypeOne = testTypeOne;
        } 
    }
}
