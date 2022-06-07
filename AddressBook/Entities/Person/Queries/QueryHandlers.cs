using AddressBook.Infrastructure.Data;
using AddressBook.Infrastructure.Messages;

namespace AddressBook.Entities.Person.Queries
{
    public class QueryHandlers : IQueryHandler<GetAllPersonQuery, IEnumerable<Person>>, IQueryHandler<GetPersonByIdQuery, Person?>,
        IQueryHandler<GetActivePersonsQuery, IEnumerable<Person>>, IQueryHandler<GetArchivedPersonsQuery, IEnumerable<Person>>
    {
        private readonly IPersonService _person;

        public QueryHandlers(IPersonService person)
        {
            this._person = person;
        }

        public Task<IEnumerable<Person>> Handle(GetAllPersonQuery query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_person.GetAllPersons());
        }

        public Task<Person?> Handle(GetPersonByIdQuery query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_person.GetPersonById(query.Id));
        }

        public Task<IEnumerable<Person>> Handle(GetActivePersonsQuery query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_person.GetAllPersons().Where(p => !p.Archived));
        }

        public Task<IEnumerable<Person>> Handle(GetArchivedPersonsQuery query, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_person.GetAllPersons().Where(p => p.Archived));
        }
    }
}
