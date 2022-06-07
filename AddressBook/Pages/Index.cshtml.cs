using AddressBook.Entities.Person;
using AddressBook.Entities.Person.Commands;
using AddressBook.Entities.Person.Queries;
using AddressBook.Infrastructure.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AddressBook.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IEnumerable<Person> Persons { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet([FromServices] QueryHandler<GetActivePersonsQuery, IEnumerable<Person>> query)
    {
        Persons = await query(new GetActivePersonsQuery());
    }

    public async Task<IActionResult> OnPost(string firstName, string lastName, [FromServices] CommandHandler<AddPersonCommand> command)
    {

        await command(new AddPersonCommand(Guid.NewGuid(), firstName, lastName));
        return RedirectToPage();
    }
}