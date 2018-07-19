using App.Common.Interface;
using App.Common.Validator;
using EasyNetQ;
using FluentValidation.Results;
using System.Threading.Tasks;
using IMessage = App.Common.Interface.IMessage;

namespace App.Common.Dispatcher
{
    public interface ICommandDispatcher
    {
        Task Send<TCommand>(TCommand command)
            where TCommand : class, ICommand;
    }

    public interface IQueryDispatcher
    {
        Task<TQueryResult> Request<TQuery, TQueryResult>(TQuery query)
            where TQuery : class, IQuery
            where TQueryResult : class, IQueryResult;
    }

    public interface IEventDispatcher
    {
        Task Publish<TEvent>(TEvent evnt)
            where TEvent : class, IEvent;
    }

    // Implementation of the command dispatcher - selects and executes the appropriate command
    public class Dispatcher : ICommandDispatcher, IQueryDispatcher, IEventDispatcher
    {
        private readonly IValidator _validator;
        private readonly IBus _bus;

        public Dispatcher(IValidator validator, IBus bus)
        {
            _bus = bus;
            _validator = validator;
        }

        public async Task Send<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            Validate(command);
            await _bus.SendAsync(command.GetType().Name, command);
        }

        public async Task Publish<TEvent>(TEvent evnt) where TEvent : class, IEvent
        {
            Validate(evnt);

            await _bus.PublishAsync(evnt);
            
        }

        public async Task<TQueryResult> Request<TQuery, TQueryResult>(TQuery query)
            where TQuery : class, IQuery
            where TQueryResult : class, IQueryResult
        {
            Validate(query);
            return await _bus.RequestAsync<TQuery, TQueryResult>(query);
        }

        private void Validate<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            if (!_validator.TryValidate(message, out ValidationResult result))
            {
                var errors = string.Join(",", result.Errors);
                throw new System.InvalidOperationException(errors);
            }
        }
    }
}