using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CXY.CJS.Tests.Services
{
    public class NoMediator: IMediator
    {
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = new CancellationToken())
        {
            return  Activator.CreateInstance<TResponse>();
        }

        public Task Publish(object notification, CancellationToken cancellationToken = new CancellationToken())
        {
           return  Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = new CancellationToken()) where TNotification : INotification
        {
            return Task.CompletedTask;
        }
    }
}