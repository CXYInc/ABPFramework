using System;
using MediatR;

namespace CXY.CJS.Core.Bus.Events
{
    /// <summary>
    /// 事件，可多个订阅者，不即时回复
    /// </summary>
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}