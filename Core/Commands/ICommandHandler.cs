namespace Core.Commands
{
    public interface ICommandHandler<TCommand>
    {
        Task Handle(TCommand request, CancellationToken cancellationToken);
    }
}
