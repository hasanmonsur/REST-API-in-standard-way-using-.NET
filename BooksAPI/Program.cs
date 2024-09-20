using BooksAPI.Contacts;
using BooksAPI.Data;
using BooksAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure connection string for the database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddControllers();

// Register services and repositories
builder.Services.AddSingleton<DapperContext>();
// Configure Dependency Injection for services and repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Configure Dapper with SQL Server
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


// Enable Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Service", Version = "v1" });
});

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Service v1");
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Service v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();