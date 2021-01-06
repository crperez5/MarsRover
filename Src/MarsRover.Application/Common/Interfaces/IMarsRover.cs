using MarsRover.Application.Common.Enums;

namespace MarsRover.Application.Common.Interfaces
{
    public interface IMarsRover
    {
        int X { get; }
        int Y { get; }

        Direction Direction { get; }

        void MoveForward();
    }
}
