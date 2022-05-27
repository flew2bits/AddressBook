using AddressBook.Infrastructure.Data;
using AddressBook.Infrastructure.Messages;

namespace AddressBook.Entities.Person.Commands;

public class CommandHandlers
    : ICommandHandler<AddPersonCommand>,
        ICommandHandler<AddPhoneNumberToPersonCommand>,
        ICommandHandler<RemovePhoneNumberFromPersonCommand>
{
    private readonly IPersonService _personService;

    public CommandHandlers(IPersonService personService)
    {
        _personService = personService;
    }

    public Task Handle(AddPersonCommand command, CancellationToken cancellationToken = default)
    {
        _personService.AddPerson(new Person(command.Id, command.FirstName, command.LastName, Array.Empty<PhoneNumber>()));
        return Task.CompletedTask;
    }
    
    public Task Handle(AddPhoneNumberToPersonCommand command, CancellationToken cancellationToken = default)
    {
        var person = _personService.GetPersonById(command.PersonId);
        if (person is null) throw new InvalidOperationException("could not find person");
        if (person.PhoneNumbers.Any(p => p.Number == command.PhoneNumber)) return Task.CompletedTask;
        
        person = person with
        {
            PhoneNumbers = person.PhoneNumbers.Append(new PhoneNumber(command.PhoneNumber, command.Type))
        };
        _personService.UpdatePerson(person);

        return Task.CompletedTask;
    }

    public Task Handle(RemovePhoneNumberFromPersonCommand command, CancellationToken cancellationToken = default)
    {
        var person = _personService.GetPersonById(command.PersonId);
        if (person is null) throw new InvalidOperationException("could not find person");
        var matching = person.PhoneNumbers.Where(p => p.Number == command.PhoneNumber);

        person = person with
        {
            PhoneNumbers = person.PhoneNumbers.Except(matching)
        };
        
        _personService.UpdatePerson(person);

        return Task.CompletedTask;
    }
}