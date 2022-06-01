using AddressBook.Infrastructure.Data;
using AddressBook.Infrastructure.Messages;

namespace AddressBook.Entities.Person.Commands;

public class CommandHandlers: 
    ICommandHandler<AddPersonCommand>,
    ICommandHandler<AddAddressToPerson>,
    ICommandHandler<DeleteAddressFromPerson>
{
    private readonly IPersonService _personService;

    public CommandHandlers(IPersonService personService)
    {
        _personService = personService;
    }

    public Task Handle(AddPersonCommand command, CancellationToken cancellationToken = default)
    {
        _personService.AddPerson(new Person(command.Id, command.FirstName, command.LastName, Array.Empty<Address>()));
        return Task.CompletedTask;
    }

    public Task Handle(AddAddressToPerson command, CancellationToken cancellationToken = default)
    {
        var person = _personService.GetPersonById(command.Id);
        if (person is null) throw new InvalidOperationException("could not find person");
        person = person with { Addresses = person.Addresses.Append(new Address(command.Street, command.ZipCode)).ToArray() };
        _personService.UpdatePerson(person);
        return Task.CompletedTask;
    }

    public Task Handle(DeleteAddressFromPerson command, CancellationToken cancellationToken = default)
    {
        var person = _personService.GetPersonById(command.PersonId);
        if (person is null) throw new InvalidOperationException("could not find person");
        var addresses = person.Addresses.OrderByDescending(a => a.Street == command.Street && a.ZipCode == command.ZipCode)
            .Skip(1).ToArray();

        person = person with { Addresses = addresses };
        _personService.UpdatePerson(person);
        return Task.CompletedTask;
    }
}