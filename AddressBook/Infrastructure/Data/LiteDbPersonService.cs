using AddressBook.Entities.Person;
using LiteDB;

namespace AddressBook.Infrastructure.Data;

public sealed class LiteDbPersonService : IPersonService, IDisposable
{
    private readonly LiteDatabase? _db;
    private readonly ILiteCollection<Person> _collection;

    public LiteDbPersonService()
    {
        _db = new LiteDatabase(@"addresses.db");
        _collection = _db.GetCollection<Person>("persons");
        _collection.EnsureIndex(p => p.FirstName);
        _collection.EnsureIndex(p => p.LastName);
    }

    private bool Exists(Guid id) => _collection.Exists(p => p.PersonId == id);
    
    public Person? GetPersonById(Guid id)
    => _collection.FindById(id);

    public IEnumerable<Person> SearchPersons(string search)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Person> GetAllPersons() => _collection.FindAll();

    public void AddPerson(Person newPerson)
    {
        if (newPerson.PersonId == Guid.Empty) throw new InvalidOperationException("person id is invalid");
        if (Exists(newPerson.PersonId)) throw new InvalidOperationException("person already exists");
        _collection.Insert(newPerson);
    }

    public void UpdatePerson(Person person)
    {
        if (!Exists(person.PersonId)) throw new InvalidOperationException("person does not exist");
        _collection.Update(person);
    }

    public void Dispose()
    {
        _db?.Dispose();
    }
}