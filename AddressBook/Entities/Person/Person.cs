namespace AddressBook.Entities.Person;

public record Person(Guid PersonId, string FirstName, string LastName, IEnumerable<PhoneNumber> PhoneNumbers);
