using System.Threading;
using System.Threading.Tasks;
using Abp.Dependency;
using CXY.CJS.Core.Bus.Commands;
using CXY.CJS.Core.Bus.Events;
using MediatR;

namespace CXY.CJS.Core.Bus
{
    public class Bus : IBus,ITransientDependency
    {
        private readonly IMediator _mediator;

        public Bus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Send(Command command, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _mediator.Send(command);
        }

        public Task<TResponse> Send<TResponse>(Command<TResponse> command, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _mediator.Send<TResponse>(command);
        }

        public Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default(CancellationToken)) where TEvent : Event
        {
            return _mediator.Publish(@event);
        }
    }
}