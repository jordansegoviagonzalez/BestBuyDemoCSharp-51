using System.Data;
using Dapper;
using BestBuyDemoCSharp_51.Data;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*A collection of services for the application to compose.
 This is useful for adding user provided or framework provided services.*/
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDbConnection> ((s) =>
/*Registering services in the built-in Dependency Injection, container
 * so ASP.NET Core can create and inject them automatically where needed meaing
 * the constructor receives IDbConnection instance automatically,
 * because the DI container builds and injects the connection whenever it sees IDbConnection requested.
 */
    {
        IDbConnection connection = new MySqlConnection(builder.Configuration.GetConnectionString("bestbuy"));
        connection.Open();
        return connection;
    });

builder.Services.AddTransient<IProductRepository, ProductRepository>();
/* Transient = build a non-permanent instance that just passes through.
 * Creates a fresh one each time it’s needed and then discarded.
 */


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();