using AddressBook.Entities.Person;

namespace AddressBook.Infrastructure.Data;

public interface IPersonService
{
    Person? GetPersonById(Guid id);
    IEnumerable<Person> SearchPersons(string search);
    IEnumerable<Person> GetAllPersons();
    void AddPerson(Person newPerson);
    void UpdatePerson(Person newPerson);
}