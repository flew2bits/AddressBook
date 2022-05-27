using System.Collections.Concurrent;
using AddressBook.Entities.Person;

namespace AddressBook.Infrastructure.Data;

public class PersonService : IPersonService
{
    private readonly ConcurrentDictionary<Guid, Person> _data = new();

    public Person? GetPersonById(Guid id)
        => _data.TryGetValue(id, out var person) ? person : null;

    public IEnumerable<Person> SearchPersons(string search) =>
        _data.Values.Where(v =>
            v.FirstName.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
            v.LastName.Contains(search, StringComparison.InvariantCultureIgnoreCase));

    public IEnumerable<Person> GetAllPersons() => _data.Values;

    public void AddPerson(Person newPerson)
    {
        if (newPerson.PersonId == Guid.Empty) throw new InvalidOperationException("person must have a valid id");
        if (_data.ContainsKey(newPerson.PersonId)) throw new InvalidOperationException("person already exists");
        if (!_data.TryAdd(newPerson.PersonId, newPerson)) throw new InvalidOperationException("could not add person");
    }

    public void UpdatePerson(Person person)
    {
        if (!_data.ContainsKey(person.PersonId)) throw new InvalidOperationException("person doesn't exist");
        _data[person.PersonId] = person;
    }
}