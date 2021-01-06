namespace MarsRover.Application.Common.Interfaces
{
    public interface ICommandPipeline
    {
        ICommandPipeline Pipe(ICommandHandler handler);

        void Run(ICommand command);
    }
}
