using MarsRover.Application;
using MarsRover.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MarsRover.Tests.Unit
{
    public class UnknownCommandHandlerTests
    {
        [Fact]
        public void UnknownCommandHandler_CanHandle_ReturnsTrue()
        {
            // Arrange
            var logger = new Mock<ILogger<UnknownCommandHandler>>();
            var handler = new UnknownCommandHandler(logger.Object);
            var command = new Mock<ICommand>();
            
            // Act
            var result = handler.CanHandle(command.Object);

            // Assert
            Assert.True(result);
        }        
    }
}