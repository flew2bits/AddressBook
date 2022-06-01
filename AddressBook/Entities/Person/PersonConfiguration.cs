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
        services.AddCommandHandler<AddAddressToPersonCommand, CommandHandlers>();
        services.AddCommandHandler<DeleteAddressFromPersonCommand, CommandHandlers>();
        services.AddCommandHandler<AddPhoneNumberCommand, CommandHandlers>();
        services.AddCommandHandler<DeletePhoneNumberFromPerson, CommandHandlers>();
        services.AddCommandHandler<AddSocialMediaCommand, CommandHandlers>();
        return services;
    }
}