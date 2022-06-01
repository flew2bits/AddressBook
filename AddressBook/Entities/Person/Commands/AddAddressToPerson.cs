namespace AddressBook.Entities.Person.Commands;

public record AddAddressToPerson(Guid Id, string Street, string ZipCode);