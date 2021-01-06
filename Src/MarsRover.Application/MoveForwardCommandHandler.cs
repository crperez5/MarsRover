using MarsRover.Application.Common.Exceptions;
using MarsRover.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace MarsRover.Application
{
    public class MoveForwardCommandHandler : ICommandHandler
    {
        private readonly IMarsRover _rover;
        private readonly ILogger<MoveForwardCommandHandler> _logger;

        public MoveForwardCommandHandler(IMarsRover rover, ILogger<MoveForwardCommandHandler> logger)
        {
            _rover = rover;
            _logger = logger;
        }

        public bool CanHandle(ICommand command) => command.Id == "F";

        public void Handle(ICommand command)
        {
            try
            {
                _rover.MoveForward();
                _logger.LogInformation("Rover moved forward. Current status: {@rover}", _rover);
            }
            catch (CoordinatesOutOfBoundsException)
            {
                _logger.LogWarning("Rover could not be moved forward since it has reached the end of Mars");
            }
        }
    }
}
