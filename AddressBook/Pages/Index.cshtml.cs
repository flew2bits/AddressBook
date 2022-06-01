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
    
    public IEnumerable<Person>? Persons { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet([FromServices] QueryHandler<GetAllPersonsQuery, IEnumerable<Person>> allQuery,
        [FromServices] QueryHandler<SearchPersonsQuery, IEnumerable<Person>> searchQuery)
    {
        Persons = Search is null
            ? await allQuery(new GetAllPersonsQuery())
            : await searchQuery(new SearchPersonsQuery(Search));
    }

    public async Task<IActionResult> OnPost(string firstName, string lastName,
        [FromServices] CommandHandler<AddPersonCommand> command)
    {
        await command(new AddPersonCommand(Guid.NewGuid(), firstName, lastName));
        return RedirectToPage();
    }
}