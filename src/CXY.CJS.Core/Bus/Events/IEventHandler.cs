using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace CXY.CJS.Core.Bus.Events
{
    public interface IEventHandler<TEvent> :IRequestHandler<TEvent> where TEvent : Event
    {
    }

  
}
