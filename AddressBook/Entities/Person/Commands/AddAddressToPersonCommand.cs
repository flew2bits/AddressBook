namespace AddressBook.Entities.Person.Commands
{
    public record AddAddressToPersonCommand(Guid Id, string Street, string ZipCode);
    
}
