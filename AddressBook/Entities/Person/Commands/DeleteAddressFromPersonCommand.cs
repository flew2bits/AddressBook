namespace AddressBook.Entities.Person.Commands
{
    public record DeleteAddressFromPersonCommand(Guid Id, string Street, string ZipCode);
    
}
