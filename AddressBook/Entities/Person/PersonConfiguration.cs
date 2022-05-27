using AddressBook.Entities.Person.Commands;
using AddressBook.Entities.Person.Queries;
using AddressBook.Infrastructure.Messages;

namespace AddressBook.Entities.Person;

public static class PersonConfiguration
{
    public static IServiceCollection AddPersonEntity(this IServiceCollection services)
        => services
            .AddQueryHandler<GetAllPersonsQuery, IEnumerable<Person>, QueryHandlers>()
            .AddQueryHandler<GetPersonByIdQuery, Person?, QueryHandlers>()
            .AddCommandHandler<AddPersonCommand, CommandHandlers>();
}