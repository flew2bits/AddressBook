namespace AddressBook.Entities.Person.Commands;

public record RemovePhoneNumberFromPersonCommand(Guid PersonId, string PhoneNumber);
