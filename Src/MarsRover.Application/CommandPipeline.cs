using MarsRover.Application.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace MarsRover.Application
{
    public class CommandPipeline : ICommandPipeline
    {
        private readonly List<ICommandHandler> _handlers = new List<ICommandHandler>();

        public ICommandPipeline Pipe(ICommandHandler handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException("Cannot pipe a null handler");
            }

            _handlers.Add(handler);
            return this;
        }

        public void Run(ICommand command)
        {
            foreach (var handler in _handlers)
            {
                if (handler.CanHandle(command))
                {
                    handler.Handle(command);
                    break;
                }
            }
        }
    }
}
