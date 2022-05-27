using AddressBook.Entities.Person;
using LiteDB;
using Microsoft.Extensions.Options;

namespace AddressBook.Infrastructure.Data;

public class LiteDbPersonService : IPersonService
{
    static LiteDbPersonService()
    {
        BsonMapper.Global.Entity<Person>()
            .Id(p => p.Id)
            .Ctor(doc => new Person(doc["_id"].AsGuid, doc["FirstName"].AsString, doc["LastName"].AsString));
    }

    private readonly ILiteCollection<Person> _collection;

    public LiteDbPersonService(IOptions<LiteDbPersonServiceOptions> options)
    {
        var db = new LiteDatabase(options.Value.DatabasePath);
        _collection = db.GetCollection<Person>();
        _collection.EnsureIndex(p => p.FirstName);
        _collection.EnsureIndex(p => p.LastName);
    }

    private bool Exists(Guid id) => _collection.Exists(p => p.Id == id);
    
    public Person? GetPersonById(Guid id) => _collection.FindById(id);

    public IEnumerable<Person> SearchPersons(string search)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Person> GetAllPersons() => _collection.FindAll();

    public void AddPerson(Person newPerson)
    {
        if (newPerson.Id == Guid.Empty) throw new InvalidOperationException("person id is invalid");
        if (Exists(newPerson.Id)) throw new InvalidOperationException("person already exists");
        _collection.Insert(newPerson);
    }
}