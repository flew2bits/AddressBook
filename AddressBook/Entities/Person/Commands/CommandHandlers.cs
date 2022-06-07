﻿using AddressBook.Infrastructure.Data;
using AddressBook.Infrastructure.Messages;

namespace AddressBook.Entities.Person.Commands
{
    public class CommandHandlers : ICommandHandler<AddPersonCommand>, ICommandHandler<AddAddressToPersonCommand>, ICommandHandler<DeleteAddressFromPersonCommand>, ICommandHandler<AddPhoneNumberCommand>, ICommandHandler<DeletePhoneNumberFromPerson>, ICommandHandler<AddSocialMediaCommand>, ICommandHandler<DeleteSocialMediaFromPerson>

    {
        private readonly IPersonService _personService;

        public CommandHandlers(IPersonService person)
        {
            this._personService = person;
        }
        public Task Handle(AddPersonCommand command, CancellationToken cancellationToken = default)
        {
            _personService.AddPerson(new Person(command.Id, command.FirstName, command.LastName, 
                Array.Empty<Address>(), Array.Empty<PhoneNumber>(), Array.Empty<SocialMedia>(),
                false));
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



        public Task Handle(DeletePhoneNumberFromPerson command, CancellationToken cancellationToken = default)
        {
            var match = _personService.GetPersonById(command.Id);
            if (match is null) throw new InvalidOperationException("Could Not Find Person");
            match = match with
            {
                PhoneNumbers = match.PhoneNumbers
                .Where(phone => !(phone.Number == command.PhoneNumber))
                .ToArray()
            };
            _personService.UpdatePerson(match);
            return Task.CompletedTask;
        }

        public Task Handle(AddSocialMediaCommand command, CancellationToken cancellationToken = default)
        {
            var match = _personService.GetPersonById(command.Id);
            if (match is null) throw new InvalidOperationException("Could Not Find Person");
            match = match with { SocialMedia = match.SocialMedia.Append(new SocialMedia(command.Type, command.Username)).ToArray() };
            _personService.UpdatePerson(match);
            return Task.CompletedTask;
        }

        public Task Handle(DeleteSocialMediaFromPerson command, CancellationToken cancellationToken = default)
        {
            var match = _personService.GetPersonById(command.Id);
            if (match is null) throw new InvalidOperationException("Could Not Find Person");
            match = match with
            {
                SocialMedia = match.SocialMedia
                .Where(socialMedia => !(socialMedia.Username == command.Username && socialMedia.Type == command.Type))
                .ToArray()
            };
            _personService.UpdatePerson(match);
            return Task.CompletedTask;
        }
    }
}
