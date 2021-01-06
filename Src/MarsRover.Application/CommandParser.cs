using MarsRover.Application.Common.Exceptions;
using MarsRover.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarsRover.Application
{
    public class CommandParser : ICommandParser
    {
        private readonly Regex _defaultRegex = new Regex(@"^[a-zA-Z\s]+$");
       
        public IEnumerable<ICommand> Parse(string rawInput)
        {
            if (string.IsNullOrEmpty(rawInput))
            {
                return Enumerable.Empty<ICommand>();
            }

            if (!_defaultRegex.IsMatch(rawInput))
            {
                throw new ValidationException();
            }

            var commands = rawInput
                .ToCharArray()
                .Where(c => char.IsLetter(c))
                .Select(c => new Command(c.ToString().ToUpper()));

            return commands;
        }
    }
}
