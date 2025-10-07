using KQRegistrationWeb.Models;
using KQRegistratrationWeb.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISchoolsRepository, KQSchoolsRepository>();
var app = builder.Build();

app.UseStaticFiles();
 
app.UseHttpsRedirection();

app.UseAuthorization();
 
 
app.MapDefaultControllerRoute();
app.Run();
