using System.Collections.Generic;

namespace MarsRover.Application.Common.Interfaces
{
    public interface ICommandParser
    {
        IEnumerable<ICommand> Parse(string rawInput);
    }
}
