using App.Common.Interface;
using Autofac;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App.Common.Executor

{
    public interface ICommandExecutor
    {
        Task ExecuteCommand<T>(T command) where T : ICommand;
    }

    public interface IEventExecutor
    {
        Task ExecuteEvent<T>(T evnt) where T : IEvent;
    }

    public interface IQueryExecutor
    {
        Task<TResult> ExecuteQuery<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : IQueryResult;
    }

    public class ExecutorLogger
    {
    }

    public class Executor : ICommandExecutor, IEventExecutor, IQueryExecutor
    {
        private readonly IComponentContext _resolver;

        public Executor(IComponentContext resolver)
        {
            _resolver = resolver;
        }

        public async Task ExecuteCommand<T>(T command) where T : ICommand
        {
            if (!_resolver.IsRegistered<ICommandHandler<T>>())
            {
                throw new System.Exception($"No handler could be found for {command.GetType().Name}");
            }

            var handler = _resolver.Resolve<ICommandHandler<T>>();

            var stopwatch = Stopwatch.StartNew();
            try
            {
                await handler.Execute(command);
            }
            catch (System.Exception e)
            {
                //TODO: Logging
                throw;
            }
            stopwatch.Stop();
        }

        public async Task<TResult> ExecuteQuery<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : IQueryResult
        {
            if (!_resolver.IsRegistered<IQueryHandler<TQuery, TResult>>())
            {
                throw new System.Exception($"No handler could be found for {query.GetType().Name}");
            }

            var handler = _resolver.Resolve<IQueryHandler<TQuery, TResult>>();

            var stopwatch = Stopwatch.StartNew();
            TResult result;
            try
            {
                result = await handler.Execute(query);
            }
            catch (System.Exception e)
            {
                throw;
            }

            stopwatch.Stop();

            return result;
        }

        public async Task ExecuteEvent<T>(T evnt) where T : IEvent
        {
            if (!_resolver.IsRegistered<IEventHandler<T>>())
            {
                return;
            }

            var handlers = _resolver.Resolve<IEnumerable<IEventHandler<T>>>();
            foreach (var handler in handlers)
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    await handler.Handle(evnt);
                }
                catch (System.Exception e)
                {
                    //TODO: Logging
                    throw;
                }
                stopwatch.Stop();
            }
        }
    }
}