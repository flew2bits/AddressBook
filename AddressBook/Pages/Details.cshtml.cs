using AddressBook.Entities.Person;
using AddressBook.Entities.Person.Commands;
using AddressBook.Entities.Person.Queries;
using AddressBook.Infrastructure.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddressBook.Pages;

public class Details : PageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid PersonId { get; set; }
    
    public Person? Person { get; set; }
    
    public async Task<IActionResult> OnGet([FromServices] QueryHandler<GetPersonByIdQuery, Person?> query)
    {
        Person = await query(new GetPersonByIdQuery(PersonId));
        return Person is not null ? Page() : NotFound();
    }

    public async Task<IActionResult> OnPostAddAddress(string street, string zipCode, 
        [FromServices] CommandHandler<AddAddressToPerson> command)
    {
        await command(new AddAddressToPerson(PersonId, street, zipCode));
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAddress(string street, string zipCode,
        [FromServices] CommandHandler<DeleteAddressFromPerson> command)
    {
        await command(new DeleteAddressFromPerson(PersonId, street, zipCode));
        return RedirectToPage();
    }
}