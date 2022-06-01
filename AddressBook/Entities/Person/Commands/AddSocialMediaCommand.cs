namespace AddressBook.Entities.Person.Commands
{
    public record AddSocialMediaCommand(Guid Id, string Type, string Username);
   
}
