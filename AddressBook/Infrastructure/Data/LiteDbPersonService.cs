﻿using AddressBook.Entities.Person;
using LiteDB;
using Microsoft.Extensions.Options;

namespace AddressBook.Infrastructure.Data;

public sealed class LiteDbPersonService : IPersonService, IDisposable
{
    static LiteDbPersonService()
    {
        BsonMapper.Global.Entity<Person>()
            .Id(p => p.Id)
            .Ctor(doc => new Person(doc["_id"].AsGuid, doc["FirstName"].AsString, doc["LastName"].AsString, Array.Empty<Address>()));
    }

    private readonly LiteDatabase _db;
    private readonly ILiteCollection<Person> _collection;

    public LiteDbPersonService(IOptions<LiteDbPersonServiceOptions> options)
    {
        _db = new LiteDatabase(options.Value.DatabasePath);
        _collection = _db.GetCollection<Person>();
        _collection.EnsureIndex(p => p.FirstName);
        _collection.EnsureIndex(p => p.LastName);
    }

    private bool Exists(Guid id) => _collection.Exists(p => p.Id == id);
    
    public Person? GetPersonById(Guid id) => _collection.FindById(id);

    public IEnumerable<Person> SearchPersons(string search)
        => _collection.Find(p =>
            p.FirstName.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
            p.LastName.Contains(search, StringComparison.InvariantCultureIgnoreCase));

    public IEnumerable<Person> GetAllPersons() => _collection.FindAll();

    public void AddPerson(Person newPerson)
    {
        if (newPerson.Id == Guid.Empty) throw new InvalidOperationException("person id is invalid");
        if (Exists(newPerson.Id)) throw new InvalidOperationException("person already exists");
        _collection.Insert(newPerson);
    }

    public void UpdatePerson(Person person)
    {
        if (!Exists(person.Id)) throw new InvalidOperationException("person does not exist");
        _collection.Update(person);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}