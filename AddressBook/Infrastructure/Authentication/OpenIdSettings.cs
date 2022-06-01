namespace AddressBook.Infrastructure.Authentication
{
    public class OpenIdSettings
    {
        public string CookieName { get; set; }
        public string CookiePath { get; set; }

        public string Authority { get; set; }

        public string ClientId { get; set; }
    }
}
