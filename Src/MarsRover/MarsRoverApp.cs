using MarsRover.Application.Common.Exceptions;
using MarsRover.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace MarsRover
{
    public class MarsRoverApp
    {
        private readonly ICommandParser _parser;
        private readonly ICommandPipeline _pipeline;
        private readonly ILogger<MarsRoverApp> _logger;

        public MarsRoverApp(ICommandParser parser, ICommandPipeline pipeline, ILogger<MarsRoverApp> logger)
        {
            _parser = parser;
            _pipeline = pipeline;
            _logger = logger;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter a command: ");

                    var rawInput = Console.ReadLine();

                    var commands = _parser.Parse(rawInput).ToList();

                    commands.ForEach(_pipeline.Run);

                    Console.WriteLine();
                }
                catch (ValidationException)
                {
                    _logger.LogError("Invalid input");
                }
            }

        }
    }
}
