using AddressBook.Infrastructure.Data;
using AddressBook.Infrastructure.Messages;

namespace AddressBook.Entities.Person.Commands
{
    public class CommandHandlers : ICommandHandler<AddPersonCommand>
    {
        private readonly PersonService person;

        public CommandHandlers(PersonService person)
        {
            this.person = person;
        }
        public Task Handle(AddPersonCommand command, CancellationToken cancellationToken = default)
        {
            person.AddPerson(new Person(command.Id, command.FirstName, command.LastName));
            return Task.CompletedTask;
        }


    }
}
