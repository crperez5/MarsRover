using MarsRover.Application.Common.Enums;
using MarsRover.Application.Common.Exceptions;
using MarsRover.Application.Common.Interfaces;

namespace MarsRover.Application
{
    public class MarsRover : IMarsRover
    {
        private readonly int _minCoordinates;
        private readonly int _maxCoordinates;

        public MarsRover(int x, int y, Direction direction, int minCoordinates, int maxCoordinates)
        {
            X = x;
            Y = y;
            Direction = direction;
            _minCoordinates = minCoordinates;
            _maxCoordinates = maxCoordinates;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public Direction Direction { get; private set; }

        public void MoveForward()
        {
            if (X + 1 > _maxCoordinates)
            {
                throw new CoordinatesOutOfBoundsException();
            }

            X += 1;
        }
    }
}
