using AddressBook.Entities.Person;
using AddressBook.Entities.Person.Queries;
using AddressBook.Infrastructure.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddressBook.Pages;

public class Archived : PageModel
{
    public IEnumerable<Person> Persons { get; set; }

    public async Task OnGet([FromServices] QueryHandler<GetArchivedPersonsQuery, IEnumerable<Person>> query)
    {
        Persons = await query(new GetArchivedPersonsQuery());
    }
}