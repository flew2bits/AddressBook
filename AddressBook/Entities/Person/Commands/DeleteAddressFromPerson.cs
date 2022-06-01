namespace AddressBook.Entities.Person.Commands;

public record DeleteAddressFromPerson(Guid PersonId, string Street, string ZipCode);