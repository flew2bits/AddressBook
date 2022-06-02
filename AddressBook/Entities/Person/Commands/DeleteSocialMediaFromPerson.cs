namespace AddressBook.Entities.Person.Commands
{
    public record DeleteSocialMediaFromPerson(Guid Id, string Type, string Username);

}