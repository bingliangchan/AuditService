using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace App.TestUtil
{
    [TestClass]
    public abstract class AutoMockFixture<T> where T : class
    {
        protected T Sut;

        protected MockRepository Mocks;
        protected AutoMockContainer Container;

        [TestInitialize]
        public virtual void Setup()
        {
            Mocks = new MockRepository(MockBehavior.Loose);

            Setup(Container = new AutoMockContainer(Mocks));

            Sut = Container.Create<T>();
        }

        protected virtual void Setup(AutoMockContainer container) { }

        [TestCleanup]
        public virtual void Teardown()
        {
            Sut = null;
            Container = null;
            Mocks = null;
        }

        protected object Resolve(Type type)
        {
            var method = Container.GetType().GetMethod("Resolve");
            var invoker = method.MakeGenericMethod(type);
            var instance = invoker.Invoke(Container, null);

            return instance;
        }

        protected Mock<TR> MockOf<TR>() where TR : class
        {
            return Container.GetMock<TR>();
        }




    }
}