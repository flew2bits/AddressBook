using AddressBook.Entities.Person;
using AddressBook.Infrastructure.Authentication;
using AddressBook.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services
    .AddPersonEntity()
    .Configure<LiteDbPersonServiceOptions>(options => options.DatabasePath = "addresses.db")

    .AddSingleton<IPersonService, LiteDbPersonService>()
    .AddRazorPages();

var openIdSettings = new OpenIdSettings();

builder.Configuration.GetSection(nameof(OpenIdSettings)).Bind(openIdSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.Cookie.Name = openIdSettings.CookieName;
        options.Cookie.Path = openIdSettings.CookiePath;

    })

    .AddOpenIdConnect(options =>
    {
        options.Authority = openIdSettings.Authority;
        options.ClientId = openIdSettings.ClientId;
    });

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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();