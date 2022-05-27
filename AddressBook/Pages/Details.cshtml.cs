using AddressBook.Entities.Person;
using AddressBook.Entities.Person.Commands;
using AddressBook.Entities.Person.Queries;
using AddressBook.Infrastructure.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddressBook.Pages
{
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

        public async Task<IActionResult> OnPostPhoneNumber(string phoneNumber, [FromServices] CommandHandler<AddPhoneNumberCommand> command)
        {
            await command(new AddPhoneNumberCommand(PersonId, phoneNumber));
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAddress(string street, string zipcode, [FromServices] CommandHandler<DeleteAddressFromPersonCommand> command)
        {
            await command(new DeleteAddressFromPersonCommand(PersonId, street, zipcode));
            return RedirectToPage();
        }
    }
}
