using App.Audit.Domain.Query;
using App.Common.Executor;
using App.Common.Interface;
using Autofac;
using EasyNetQ;

namespace App.Audit.QueryProcessor
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
            SetupToRespond<GetAuditEventQuery, GetAuditEventQueryResult>();
        }

        private void SetupToRespond<TRequest, TResponse>()
            where TRequest : class, IQuery
            where TResponse : class, IQueryResult
        {
            _bus.RespondAsync<TRequest, TResponse>(request => _resolver.Resolve<IQueryExecutor>().ExecuteQuery<TRequest, TResponse>(request));
        }
    }
}