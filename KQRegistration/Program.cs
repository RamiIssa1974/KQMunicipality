using KQApi.DbContexts;
using KQApi.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File($"c:/logs/cityinfo_{DateTime.Now.ToString("dd_MM_yyyy")}.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontends", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",           // Local Next.js dev server
            "http://creativehandsco.com" // Your deployed site
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});





builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<CityInfoContext>(
    dbContextOptions => dbContextOptions.UseSqlServer(
        builder.Configuration["ConnectionStrings:CityInfoDBConnectionString"]
        ));
//builder.Services.AddSingleton<CitiesDataStore>();



builder.Services.AddScoped<ISQLBasateenRepository, SQLBasateenRepository>();
builder.Services.AddScoped<IBasateenRepository, BasateenRepository>();

//builder.Services.AddSingleton<INamedServiceResolver, NamedServiceResolver>();
 

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
 

var app = builder.Build();
app.UseCors("AllowFrontends");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
