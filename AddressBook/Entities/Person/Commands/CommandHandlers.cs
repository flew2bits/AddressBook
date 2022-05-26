using AddressBook.Infrastructure.Data;
using AddressBook.Infrastructure.Messages;

namespace AddressBook.Entities.Person.Commands;

public class CommandHandlers
    : ICommandHandler<AddPersonCommand>
{
    private readonly PersonService _personService;

    public CommandHandlers(PersonService personService)
    {
        _personService = personService;
    }

    public Task Handle(AddPersonCommand command, CancellationToken cancellationToken = default)
    {
        _personService.AddPerson(new Person(command.Id, command.FirstName, command.LastName));
        return Task.CompletedTask;
    }
}