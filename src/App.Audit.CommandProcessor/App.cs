using App.Audit.Domain.Command;
using App.Common.Executor;
using App.Common.Interface;
using Autofac;
using EasyNetQ;

namespace App.Audit.CommandProcessor
{
    public class App
    {
        private readonly IBus _bus;
        private readonly IComponentContext _resolver;

        public App(IBus bus, IComponentContext resolver)
        {
            _bus = bus;
            _resolver = resolver;
        }


        public void Run()
        {
            
            // Setup commands
            Setup<AuditEventCreateCommand>();
            
        }


        private void Setup<TCommand>()
            where TCommand : class, ICommand
        {
            _bus.Receive<TCommand>(typeof(TCommand).Name, request => _resolver.Resolve<ICommandExecutor>().ExecuteCommand(request));
        }

    }
}