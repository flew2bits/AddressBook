using AddressBook.Entities.Person.Commands;
using AddressBook.Entities.Person.Queries;
using AddressBook.Infrastructure.Messages;

namespace AddressBook.Entities.Person;

public static class PersonConfiguration
{
    public static IServiceCollection AddPersonEntity(this IServiceCollection services)
    {
        services.AddQueryHandler<GetAllPersonQuery, IEnumerable<Person>, QueryHandlers>();
        services.AddQueryHandler<GetPersonByIdQuery, Person?, QueryHandlers>();
        services.AddCommandHandler<AddPersonCommand, CommandHandlers>();
        return services;
    }
}