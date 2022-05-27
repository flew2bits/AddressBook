namespace AddressBook.Entities.Person.Commands;

public record AddPhoneNumberToPersonCommand(Guid PersonId, string PhoneNumber, string Type);