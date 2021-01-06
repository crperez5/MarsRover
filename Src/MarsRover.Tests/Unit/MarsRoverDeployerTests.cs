using MarsRover.Application;
using Xunit;

namespace MarsRover.Tests.Unit
{
    public class MarsRoverDeployerTests
    {
        [Fact]
        public void MarsRoverDeployer_Deploy_RoverNotNull()
        {
            // Arrange
            var minCoord = -3;
            var maxCoord = 3;

            // Act
            var rover = MarsRoverDeployer.Deploy(minCoord, maxCoord);

            // Assert
            Assert.NotNull(rover);
        }

        [Fact]
        public void MarsRoverDeployer_Deploy_CoordinatesWithinRange()
        {
            // Arrange
            var minCoord = -3;
            var maxCoord = 3;

            // Act
            var rover = MarsRoverDeployer.Deploy(minCoord, maxCoord);

            // Assert
            Assert.True(rover.X >= minCoord && rover.X <= maxCoord);
            Assert.True(rover.Y >= minCoord && rover.Y <= maxCoord);
        }
    }
}
