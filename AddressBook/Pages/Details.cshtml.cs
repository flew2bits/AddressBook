using AddressBook.Entities.Person;
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
}