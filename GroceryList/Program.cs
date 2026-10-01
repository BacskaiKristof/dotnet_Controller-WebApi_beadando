using GroceryList.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("GroceryListContext") ?? throw new InvalidOperationException("Connection string 'GroceryListContext' not found.");



builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<GroceryListContext>(opt => opt.UseInMemoryDatabase("GroceryList"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
