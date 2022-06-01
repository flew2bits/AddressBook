using AddressBook.Entities.Person;
using AddressBook.Entities.Person.Commands;
using AddressBook.Entities.Person.Queries;
using AddressBook.Infrastructure.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddressBook.Pages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid PersonId { get; set; }

        public Person? Person { get; set; }

        public async Task<IActionResult> OnGet([FromServices] QueryHandler<GetPersonByIdQuery, Person?> query)
        {

            Person = await query(new GetPersonByIdQuery(PersonId));
            return Person is not null ? Page() : NotFound();
        }

        public async Task<IActionResult> OnPostAddresses(string street, string zipCode, [FromServices] CommandHandler<AddAddressToPersonCommand> command)
        {
            await command(new AddAddressToPersonCommand(PersonId, street, zipCode));
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostPhoneNumber(string Number, [FromServices] CommandHandler<AddPhoneNumberCommand> command)
        {
            await command(new AddPhoneNumberCommand(PersonId, Number));
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAddress(string street, string zipcode, [FromServices] CommandHandler<DeleteAddressFromPersonCommand> command)
        {
            await command(new DeleteAddressFromPersonCommand(PersonId, street, zipcode));
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeletePhoneNumber(string phoneNumber, [FromServices] CommandHandler<DeletePhoneNumberFromPerson> command)
        {
            await command(new DeletePhoneNumberFromPerson(PersonId, phoneNumber));
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSocialMedia(string type, string username, [FromServices] CommandHandler<AddSocialMediaCommand> command)
        {
            await command(new AddSocialMediaCommand(PersonId, type, username));
            return RedirectToPage();
        }


    }
}
