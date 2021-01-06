using MarsRover.Application;
using MarsRover.Application.Common.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace MarsRover.Tests.Unit
{
    public class CommandParserTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Parse_EmptyInput_ReturnsNoCommands(string rawInput)
        {
            // Arrange
            var parser = new CommandParser();

            // Act
            var result = parser.Parse(rawInput);

            // Assert
            Assert.Empty(result);
        }

        [Theory]
        [InlineData("_")]
        [InlineData("_F")]
        [InlineData("F_F")]
        [InlineData("F_")]
        [InlineData("_F_")]
        public void Parse_InvalidInput_ReturnsException(string rawInput)
        {
            // Arrange
            var parser = new CommandParser();

            // Act + Assert
            Assert.Throws<ValidationException>(() => parser.Parse(rawInput));
        }

        [Theory]
        [InlineData("F", 1)]
        [InlineData("f", 1)]
        [InlineData("FF", 2)]
        [InlineData("A", 1)]
        [InlineData("F  ", 1)]
        [InlineData("   F  ", 1)]
        [InlineData("   F", 1)]
        [InlineData("F  F", 2)]
        [InlineData(" F", 1)]
        [InlineData("F F", 2)]
        [InlineData("F ", 1)]
        [InlineData(" F ", 1)]
        public void Parse_ValidInput_ReturnsCommandsCount(string rawInput, int expectedCount)
        {
            // Arrange
            var parser = new CommandParser();

            // Act
            var results = parser.Parse(rawInput).ToList();

            // Assert
            Assert.Equal(expectedCount, results.Count());
        }

        public static TheoryData<(string rawInput, List<Command> expectedCommands)> TestData =>
            new TheoryData<(string rawInput, List<Command> expectedCommands)>
            {
               ("F", new List<Command> { new Command("F")}),
               ("f", new List<Command> { new Command("F")}),
               ("FF", new List<Command> { new Command("F"), new Command("F")}),
               ("F  ", new List<Command> { new Command("F")})
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void Parse_ValidInput_ReturnsExpectedCommands((string RawInput, List<Command> ExpectedCommands) data)
        {
            // Arrange
            var parser = new CommandParser();

            // Act
            var results = parser.Parse(data.RawInput).ToList();

            // Assert
            Assert.True(data.ExpectedCommands.All(expectedCommand => results.Contains(expectedCommand)));            
        }
    }
}