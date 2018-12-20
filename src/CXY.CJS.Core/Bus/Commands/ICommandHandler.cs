using System;
using System.Collections.Generic;
using System.Text;
using CXY.CJS.Core.Bus.Commands;
using MediatR;

namespace CXY.CJS.Core.Bus.Commands
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : Command<TResponse>
    {

    }

}
