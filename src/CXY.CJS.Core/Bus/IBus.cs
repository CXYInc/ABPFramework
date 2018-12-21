using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CXY.CJS.Core.Bus.Commands;
using CXY.CJS.Core.Bus.Events;
using MediatR;

namespace CXY.CJS.Core.Bus
{
    public interface IBus
    {
        Task Send(Command command,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<TResponse> Send<TResponse>(Command<TResponse> command,
            CancellationToken cancellationToken = default(CancellationToken));

        Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default(CancellationToken)) where TEvent : Event;
    }
}
