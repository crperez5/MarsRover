namespace MarsRover.Application.Common.Interfaces
{
    public interface ICommandHandler
    {
        bool CanHandle(ICommand command);
        void Handle(ICommand command);
    }
}
