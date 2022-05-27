using AddressBook.Entities.Person;
using AddressBook.Entities.Person.Commands;
using AddressBook.Entities.Person.Queries;
using AddressBook.Infrastructure.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddressBook.Pages;

public class Details : PageModel
{
    [BindProperty(SupportsGet = true)] public Guid PersonId { get; set; }

    public Person? Person { get; set; }

    public async Task<IActionResult> OnGet([FromServices] QueryHandler<GetPersonByIdQuery, Person?> query)
    {
        Person = await query(new GetPersonByIdQuery(PersonId));
        return Person is not null ? Page() : NotFound();
    }

    public async Task<IActionResult> OnPostAddPhoneNumber(string phoneNumber, string type,
        [FromServices] CommandHandler<AddPhoneNumberToPersonCommand> command)
    {
        if (string.IsNullOrEmpty(type)) return RedirectToPage();
        await command(new AddPhoneNumberToPersonCommand(PersonId, phoneNumber, type));
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRemovePhoneNumber(string phoneNumber,
        [FromServices] CommandHandler<RemovePhoneNumberFromPersonCommand> command)
    {
        await command(new RemovePhoneNumberFromPersonCommand(PersonId, phoneNumber));
        return RedirectToPage();
    }
}