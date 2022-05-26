﻿using AddressBook.Models;

namespace AddressBook.Infrastructure.Data;

public class DataService
{
    private Dictionary<Guid, Person> _data = new();

    public Person? GetPersonById(Guid id)
        => _data.TryGetValue(id, out var person) ? person : null;

    public IEnumerable<Person> SearchPersons(string search) =>
        _data.Values.Where(v =>
            v.FirstName.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
            v.LastName.Contains(search, StringComparison.InvariantCultureIgnoreCase));

    public IEnumerable<Person> GetAllPersons() => _data.Values;

    public void AddPerson(Person newPerson)
    {
        if (newPerson.Id == Guid.Empty) throw new InvalidOperationException("person must have a valid id");
        if (_data.ContainsKey(newPerson.Id)) throw new InvalidOperationException("person already exists");
        _data[newPerson.Id] = newPerson;
    }
}