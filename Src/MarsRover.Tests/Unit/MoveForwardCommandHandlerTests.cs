using MarsRover.Application;
using MarsRover.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MarsRover.Tests.Unit
{
    public class MoveForwardCommandHandlerTests
    {
        [Fact]
        public void CanHandle_CommandHasExpectedId_ReturnsTrue()
        {
            // Arrange
            var rover = new Mock<IMarsRover>();
            var logger = new Mock<ILogger<MoveForwardCommandHandler>>();
            var handler = new MoveForwardCommandHandler(rover.Object, logger.Object);
            var command = new Mock<ICommand>();
            command.SetupGet(c => c.Id).Returns("F");

            // Act
            var result = handler.CanHandle(command.Object);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanHandle_CommandHasNotExpectedId_ReturnsFalse()
        {
            // Arrange
            var rover = new Mock<IMarsRover>();
            var logger = new Mock<ILogger<MoveForwardCommandHandler>>();
            var handler = new MoveForwardCommandHandler(rover.Object, logger.Object);
            var command = new Mock<ICommand>();
            command.SetupGet(c => c.Id).Returns("S");

            // Act
            var result = handler.CanHandle(command.Object);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Handle_RoverIsMovedForward_ResultOk()
        {
            // Arrange
            var rover = new Mock<IMarsRover>();
            rover.SetupGet(r => r.X).Returns(0);
            rover.SetupGet(r => r.Y).Returns(0);
            var logger = new Mock<ILogger<MoveForwardCommandHandler>>();
            var handler = new MoveForwardCommandHandler(rover.Object, logger.Object);
            var command = new Mock<ICommand>();
            command.SetupGet(c => c.Id).Returns("F");

            // Act
            handler.Handle(command.Object);

            // Assert
            rover.Verify(rover => rover.MoveForward(), Times.Once);
        }              
    }
}
