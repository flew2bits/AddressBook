namespace AddressBook.Infrastructure.Messages;

public interface ICommandHandler<in TCommand>
{
    Task Handle(TCommand command, CancellationToken cancellationToken = default);
}