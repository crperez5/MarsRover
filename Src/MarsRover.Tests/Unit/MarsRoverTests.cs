using Xunit;

namespace MarsRover.Tests.Unit
{
    public class MarsRoverTests
    {
        [Fact]
        public void MoveForward_RoverIsWithinLimits_PositionUpdated()
        {
            // Arrange
            var minCoord = -10;
            var maxCoord = 10;
            var expectedX = 1;
            var expectedY = 0;
            var expectedDirection = Application.Common.Enums.Direction.EAST;

            var rover = new Application.MarsRover(0, 0, Application.Common.Enums.Direction.EAST, minCoord, maxCoord);

            // Act
            rover.MoveForward();

            // Assert
            Assert.Equal(expectedX, rover.X);
            Assert.Equal(expectedY, rover.Y);
            Assert.Equal(expectedDirection, rover.Direction);
        }

        [Fact]
        public void MoveForward_RoverIsNotWithinLimits_PositionNotUpdated()
        {
            // Arrange
            var minCoord = -10;
            var maxCoord = 10;
            var expectedX = 10;
            var rover = new Application.MarsRover(10, 0, Application.Common.Enums.Direction.EAST, minCoord, maxCoord);

            // Act
            var ex = Record.Exception(() => { rover.MoveForward(); });

            // Assert
            Assert.NotNull(ex);
            Assert.Equal(expectedX, rover.X);
        }
    }
}
