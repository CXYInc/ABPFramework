using System;
using MediatR;

namespace CXY.CJS.Core.Bus
{
    public abstract class Message : IRequest
    {
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}