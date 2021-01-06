using MarsRover.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace MarsRover.Application
{
    public class UnknownCommandHandler : ICommandHandler
    {
        private readonly ILogger<UnknownCommandHandler> _logger;

        public UnknownCommandHandler(ILogger<UnknownCommandHandler> logger)
        {
            _logger = logger;
        }
        public bool CanHandle(ICommand command) => true;

        public void Handle(ICommand command)
        {
            _logger.LogWarning($"Command {command.Id} could not be recognized");
        }
    }
}
