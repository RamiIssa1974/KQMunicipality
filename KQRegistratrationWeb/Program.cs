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
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment() )
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();
 
 
app.MapDefaultControllerRoute();
app.Run();
