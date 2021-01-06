using MarsRover.Application;
using MarsRover.Application.Common.Interfaces;
using Moq;
using Xunit;

namespace MarsRover.Tests.Unit
{
    public class CommandPipelineTests
    {
        [Fact]
        public void CommandPipeline_WhenPipelineIsRunAndHandlerCanHandle_HandlerGetsCalled()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var handler = new Mock<ICommandHandler>();
            handler.Setup(h => h.CanHandle(It.IsAny<ICommand>())).Returns(true);
            var pipeline = new CommandPipeline();
            pipeline.Pipe(handler.Object);

            // Act
            pipeline.Run(command.Object);

            // Assert
            handler.Verify(handler => handler.Handle(command.Object), Times.Once);
        }

        [Fact]
        public void CommandPipeline_WhenPipelineIsRunAndHandlerCannotHandle_HandlerDoesNotGetCalled()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var handler = new Mock<ICommandHandler>();
            handler.Setup(h => h.CanHandle(It.IsAny<ICommand>())).Returns(false);
            var pipeline = new CommandPipeline();
            pipeline.Pipe(handler.Object);

            // Act
            pipeline.Run(command.Object);

            // Assert
            handler.Verify(handler => handler.Handle(command.Object), Times.Never);
        }

        [Fact]
        public void CommandPipeline_CannotPipeNullHandler()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var pipeline = new CommandPipeline();
  
            // Act
            var ex = Record.Exception(() => pipeline.Pipe(null));

            // Assert
            Assert.NotNull(ex);
        }        
    }
}
