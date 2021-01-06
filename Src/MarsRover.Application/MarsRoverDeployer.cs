using MarsRover.Application.Common.Enums;
using MarsRover.Application.Common.Interfaces;
using System;

namespace MarsRover.Application
{
    public static class MarsRoverDeployer
    {
        public static IMarsRover Deploy(int minCoordinates, int maxCoordinates)
        {
            var random = new Random();
            var randomX = random.Next(minCoordinates, maxCoordinates + 1);
            var randomY = random.Next(minCoordinates, maxCoordinates + 1);
            var randomDirection = (Direction)random.Next(0, 4);
            return new MarsRover(randomX, randomY, randomDirection, minCoordinates, maxCoordinates);
        }
    }
}
