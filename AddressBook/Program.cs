using AddressBook.Entities.Person;
using AddressBook.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddPersonEntity()
    .Configure<LiteDbPersonServiceOptions>(options => options.DatabasePath = "bin/Debug/net6.0/addresses.db")
    .AddSingleton<IPersonService, LiteDbPersonService>()
    .AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();