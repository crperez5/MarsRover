using MarsRover.Application.Common.Interfaces;
using System;

namespace MarsRover.Application
{
    public class Command : ICommand, IEquatable<Command>
    {
        public Command(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }

        public bool Equals(Command other)
        {
            if (other == null)
                return false;

            if (this.Id == other.Id)
                return true;
            else
                return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Command commandObj = obj as Command;
            if (commandObj == null)
                return false;
            else
                return Equals(commandObj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
