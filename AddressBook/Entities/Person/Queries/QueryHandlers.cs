using AddressBook.Infrastructure.Data;
using AddressBook.Infrastructure.Messages;

namespace AddressBook.Entities.Person.Queries
{
    public class QueryHandlers : IQueryHandler<GetAllPersonQuery, IEnumerable<Person>>, IQueryHandler<GetPersonByIdQuery, Person?>
    {
        private readonly IPersonService person;

        public QueryHandlers(IPersonService person)
        {
            this.person = person;
        }

        public Task<IEnumerable<Person>> Handle(GetAllPersonQuery query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(person.GetAllPersons());
        }

        public Task<Person?> Handle(GetPersonByIdQuery query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(person.GetPersonById(query.Id));
        }
    }
}
