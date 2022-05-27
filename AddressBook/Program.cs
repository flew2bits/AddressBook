using AddressBook.Entities.Person;
using AddressBook.Infrastructure.Data;
using LiteDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddPersonEntity()
    .AddSingleton<IPersonService, LiteDbPersonService>()
    .AddRazorPages();
//
BsonMapper.Global.Entity<Person>()
    .Id(p => p.PersonId)
    .Ctor(doc =>
    {
        var phoneNumbers = doc["PhoneNumbers"].AsArray.Select(pn => new PhoneNumber(pn["Number"].AsString, pn["Type"].AsString));
        return new Person(doc["_id"].AsGuid, doc["FirstName"].AsString, doc["LastName"].AsString,
            phoneNumbers);
    });

// BsonMapper.Global.RegisterType<PhoneNumber>(
//     serialize: BsonMapper.Global.ToDocument,
//     deserialize: doc => new PhoneNumber(doc["Number"].AsString, doc["Type"].AsString));
//
//
// BsonMapper.Global.RegisterType(
//     serialize: person =>
//     {
//         var doc = new BsonDocument
//         {
//             ["PersonId"] = person.PersonId,
//             ["FirstName"] = person.FirstName,
//             ["LastName"] = person.LastName,
//             ["PhoneNumbers"] = new BsonArray(person.PhoneNumbers.Select(pn => new BsonDocument
//             {
//                 ["Number"] = pn.Number,
//                 ["Type"] = pn.Type
//             }))
//         };
//         return doc;
//     },
//     deserialize: doc => new Person(
//         doc["PersonId"].AsGuid,
//         doc["FirstName"].AsString,
//         doc["LastName"].AsString,
//         Array.Empty<PhoneNumber>()));

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