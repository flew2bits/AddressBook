using AddressBook.Infrastructure.Data;
using AddressBook.Infrastructure.Messages;

namespace AddressBook.Entities.Person.Commands
{
    public class CommandHandlers : ICommandHandler<AddPersonCommand>, ICommandHandler<AddAddressToPersonCommand>, ICommandHandler<DeleteAddressFromPersonCommand>, ICommandHandler<AddPhoneNumberCommand>
    {
        private readonly IPersonService _personService;

        public CommandHandlers(IPersonService person)
        {
            this._personService = person;
        }
        public Task Handle(AddPersonCommand command, CancellationToken cancellationToken = default)
        {
            _personService.AddPerson(new Person(command.Id, command.FirstName, command.LastName, Array.Empty<Address>(), Array.Empty<PhoneNumber>()));
            return Task.CompletedTask;
        }



        public Task Handle(AddAddressToPersonCommand command, CancellationToken cancellationToken = default)
        {
            var match = _personService.GetPersonById(command.Id);
            if (match is null) throw new InvalidOperationException("Could Not Find Person");
            match = match with { Addresses = match.Addresses.Append(new Address(command.Street, command.ZipCode)).ToArray() };
            _personService.UpdatePerson(match);
            return Task.CompletedTask;
        }

        public Task Handle(DeleteAddressFromPersonCommand command, CancellationToken cancellationToken = default)
        {
            var match = _personService.GetPersonById(command.Id);
            if (match is null) throw new InvalidOperationException("Could Not Find Person");
            match = match with
            {
                Addresses = match.Addresses
                .Where(address => !(address.Street == command.Street && address.ZipCode == command.ZipCode))
                .ToArray()
            };
            _personService.UpdatePerson(match);
            return Task.CompletedTask;
        }

        public Task Handle(AddPhoneNumberCommand command, CancellationToken cancellationToken = default)
        {
            var match = _personService.GetPersonById(command.Id);
            if (match is null) throw new InvalidOperationException("Could Not Find Person");
            match = match with { PhoneNumbers = match.PhoneNumbers.Append(new PhoneNumber(command.PhoneNumber)).ToArray() };
            _personService.UpdatePerson(match);
            return Task.CompletedTask;
        }


    }
}
