using Shop.API;
using Shop.Application;
using Shop.DataAccess.LightDB;
using Shop.DataAccess.SQLDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddApplication();
builder.Services.AddLightDb(builder.Configuration);
builder.Services.AddSqlDb(builder.Configuration);

var app = builder.Build();

app.Services.MigrateAndSeedSqlDb();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
