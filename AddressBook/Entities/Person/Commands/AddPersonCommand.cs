namespace AddressBook.Entities.Person.Commands
{
    public record AddPersonCommand(Guid Id, string FirstName, string LastName);

}
