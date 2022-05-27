using AddressBook.Infrastructure.Data;
using AddressBook.Infrastructure.Messages;

namespace AddressBook.Entities.Person.Queries;

public class QueryHandlers : 
    IQueryHandler<GetAllPersonsQuery, IEnumerable<Person>>,
    IQueryHandler<GetPersonByIdQuery, Person?>
{
    private readonly IPersonService _personService;

    public QueryHandlers(IPersonService personService)
    {
        _personService = personService;
    }

    public Task<IEnumerable<Person>> Handle(GetAllPersonsQuery query, CancellationToken cancellationToken = default)
        => Task.FromResult(_personService.GetAllPersons());

    public Task<Person?> Handle(GetPersonByIdQuery query, CancellationToken cancellationToken = default)
        => Task.FromResult(_personService.GetPersonById(query.Id));
}