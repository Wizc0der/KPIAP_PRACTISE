using PhoneBook.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ContactRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contacts}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "search",
    pattern: "Contacts/Search/{name?}",
    defaults: new { controller = "Contacts", action = "Search" });

app.Run();
