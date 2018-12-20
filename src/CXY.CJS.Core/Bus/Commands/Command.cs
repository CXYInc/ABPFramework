using System;
using MediatR;

namespace CXY.CJS.Core.Bus.Commands
{
    /// <summary>
    /// 命令
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class Command<TResponse> : Message, IRequest<TResponse>
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}